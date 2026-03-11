using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json;
using cdr_group.Contracts.DTOs.AuditLog;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Attributes;
using cdr_group.Domain.Entities.Base;
using cdr_group.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace cdr_group.Persistence.Services
{
    public class AuditDisplayNameResolver : IAuditDisplayNameResolver
    {
        private readonly ApplicationDbContext _dbContext;

        private static readonly ConcurrentDictionary<string, Dictionary<string, AuditDisplayNameAttribute>> _metadataCache = new();
        private static readonly Assembly _domainAssembly = typeof(BaseEntity).Assembly;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public AuditDisplayNameResolver(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ResolveDisplayNamesAsync(List<AuditLogDto> dtos)
        {
            // Step 1: Collect all IDs that need resolution, grouped by target entity type
            var idsToResolve = new Dictionary<Type, HashSet<string>>();

            foreach (var dto in dtos)
            {
                var metadata = GetDisplayNameMetadata(dto.EntityName);
                if (metadata.Count == 0) continue;

                CollectIds(dto.OldValues, metadata, idsToResolve);
                CollectIds(dto.NewValues, metadata, idsToResolve);
            }

            if (idsToResolve.Count == 0) return;

            // Step 2: Batch query each entity type to get display names
            // Key: "EntityType:Id", Value: (En, Ar)
            var nameLookup = new Dictionary<string, (string En, string Ar)>();

            foreach (var (entityType, ids) in idsToResolve)
            {
                var guidIds = ids
                    .Where(id => Guid.TryParse(id, out _))
                    .Select(Guid.Parse)
                    .ToHashSet();

                if (guidIds.Count == 0) continue;

                var resolved = await QueryDisplayNamesAsync(entityType, guidIds);
                foreach (var (id, names) in resolved)
                {
                    nameLookup[$"{entityType.Name}:{id}"] = names;
                }
            }

            if (nameLookup.Count == 0) return;

            // Step 3: Build display values for each DTO
            foreach (var dto in dtos)
            {
                var metadata = GetDisplayNameMetadata(dto.EntityName);
                if (metadata.Count == 0) continue;

                dto.OldDisplayValues = BuildDisplayValues(dto.OldValues, metadata, nameLookup);
                dto.NewDisplayValues = BuildDisplayValues(dto.NewValues, metadata, nameLookup);
            }
        }

        private static Dictionary<string, AuditDisplayNameAttribute> GetDisplayNameMetadata(string entityName)
        {
            return _metadataCache.GetOrAdd(entityName, name =>
            {
                var entityType = _domainAssembly.GetTypes()
                    .FirstOrDefault(t => t.Name == name && typeof(BaseEntity).IsAssignableFrom(t));

                if (entityType == null) return new();

                return entityType.GetProperties()
                    .Select(p => (p.Name, Attr: p.GetCustomAttribute<AuditDisplayNameAttribute>()))
                    .Where(x => x.Attr != null)
                    .ToDictionary(x => x.Name, x => x.Attr!);
            });
        }

        private static void CollectIds(
            string? valuesJson,
            Dictionary<string, AuditDisplayNameAttribute> metadata,
            Dictionary<Type, HashSet<string>> idsToResolve)
        {
            if (string.IsNullOrEmpty(valuesJson)) return;

            using var doc = JsonDocument.Parse(valuesJson);
            foreach (var (propName, attr) in metadata)
            {
                var camelName = char.ToUpperInvariant(propName[0]) + propName[1..];
                if (doc.RootElement.TryGetProperty(camelName, out var value))
                {
                    var idStr = value.ToString();
                    if (!string.IsNullOrEmpty(idStr))
                    {
                        if (!idsToResolve.ContainsKey(attr.EntityType))
                            idsToResolve[attr.EntityType] = new HashSet<string>();
                        idsToResolve[attr.EntityType].Add(idStr);
                    }
                }
            }
        }

        private async Task<Dictionary<string, (string En, string Ar)>> QueryDisplayNamesAsync(
            Type entityType, HashSet<Guid> ids)
        {
            var result = new Dictionary<string, (string En, string Ar)>();

            // Get the metadata for display properties from any attribute that references this entity type
            var (displayPropEn, displayPropAr) = GetDisplayPropertiesForType(entityType);
            if (displayPropEn == null) return result;

            var propInfoEn = entityType.GetProperty(displayPropEn);
            var propInfoAr = entityType.GetProperty(displayPropAr!);
            if (propInfoEn == null) return result;

            // Use reflection to call DbContext.Set<T>().Where(e => ids.Contains(e.Id)).ToListAsync()
            var setMethod = typeof(ApplicationDbContext)
                .GetMethods()
                .First(m => m.Name == "Set" && m.IsGenericMethod && m.GetParameters().Length == 0)
                .MakeGenericMethod(entityType);

            var dbSet = setMethod.Invoke(_dbContext, null);

            // Build: entities.Where(e => ids.Contains(e.Id)).IgnoreQueryFilters().ToListAsync()
            var queryable = (IQueryable<BaseEntity>)dbSet!;
            var entities = await queryable
                .IgnoreQueryFilters()
                .Where(e => ids.Contains(e.Id))
                .ToListAsync();

            foreach (var entity in entities)
            {
                var nameEn = propInfoEn.GetValue(entity)?.ToString() ?? "";
                var nameAr = propInfoAr?.GetValue(entity)?.ToString() ?? nameEn;
                result[entity.Id.ToString()] = (nameEn, nameAr);
            }

            return result;
        }

        private static (string? En, string? Ar) GetDisplayPropertiesForType(Type entityType)
        {
            // Search all cached metadata to find an attribute that references this entity type
            foreach (var metadata in _metadataCache.Values)
            {
                foreach (var attr in metadata.Values)
                {
                    if (attr.EntityType == entityType)
                        return (attr.DisplayPropertyEn, attr.DisplayPropertyAr);
                }
            }

            // If not cached yet, scan all entity types
            foreach (var type in _domainAssembly.GetTypes().Where(t => typeof(BaseEntity).IsAssignableFrom(t)))
            {
                foreach (var prop in type.GetProperties())
                {
                    var attr = prop.GetCustomAttribute<AuditDisplayNameAttribute>();
                    if (attr?.EntityType == entityType)
                        return (attr.DisplayPropertyEn, attr.DisplayPropertyAr);
                }
            }

            return (null, null);
        }

        private static string? BuildDisplayValues(
            string? valuesJson,
            Dictionary<string, AuditDisplayNameAttribute> metadata,
            Dictionary<string, (string En, string Ar)> nameLookup)
        {
            if (string.IsNullOrEmpty(valuesJson)) return null;

            using var doc = JsonDocument.Parse(valuesJson);
            var displayValues = new Dictionary<string, object>();

            foreach (var (propName, attr) in metadata)
            {
                var camelName = char.ToUpperInvariant(propName[0]) + propName[1..];
                if (doc.RootElement.TryGetProperty(camelName, out var value))
                {
                    var idStr = value.ToString();
                    var key = $"{attr.EntityType.Name}:{idStr}";
                    if (nameLookup.TryGetValue(key, out var names))
                    {
                        // Use property name without "Id" suffix (e.g. "CompanyId" -> "Company")
                        var displayKey = camelName.EndsWith("Id", StringComparison.Ordinal)
                            ? camelName[..^2]
                            : camelName;
                        displayValues[displayKey] = new { en = names.En, ar = names.Ar };
                    }
                }
            }

            return displayValues.Count > 0
                ? JsonSerializer.Serialize(displayValues, _jsonOptions)
                : null;
        }
    }
}
