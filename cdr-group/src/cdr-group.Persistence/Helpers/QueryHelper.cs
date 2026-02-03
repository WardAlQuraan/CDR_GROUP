using System.Linq.Expressions;
using cdr_group.Contracts.DTOs.Common;

namespace cdr_group.Persistence.Helpers
{
    public static class QueryHelper
    {
        public static IQueryable<T> ApplySearch<T>(IQueryable<T> query, PagedRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SearchTerm) || request.SearchProperties?.Any() != true)
                return query;

            var searchTerm = request.SearchTerm;
            var searchTermLower = searchTerm.ToLower();
            var parameter = Expression.Parameter(typeof(T), "e");
            Expression? combinedExpression = null;

            foreach (var propertyName in request.SearchProperties)
            {
                var property = typeof(T).GetProperty(propertyName);
                if (property == null) continue;

                var propertyAccess = Expression.Property(parameter, property);
                var propertyExpression = BuildPropertySearchExpression(propertyAccess, property.PropertyType, searchTerm, searchTermLower);

                if (propertyExpression != null)
                {
                    combinedExpression = combinedExpression == null
                        ? propertyExpression
                        : Expression.OrElse(combinedExpression, propertyExpression);
                }
            }

            if (combinedExpression != null)
            {
                var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
                query = query.Where(lambda);
            }

            return query;
        }

        public static IQueryable<T> ApplySort<T>(IQueryable<T> query, PagedRequest request, Expression<Func<T, object>> defaultSort)
        {
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                var property = typeof(T).GetProperties().FirstOrDefault(p=> request.SortBy.Equals(p.Name , StringComparison.OrdinalIgnoreCase));
                if (property != null)
                {
                    var parameter = Expression.Parameter(typeof(T), "e");
                    var propertyAccess = Expression.Property(parameter, property);
                    var lambda = Expression.Lambda(propertyAccess, parameter);

                    var methodName = request.SortDescending ? "OrderByDescending" : "OrderBy";
                    var method = typeof(Queryable).GetMethods()
                        .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                        .MakeGenericMethod(typeof(T), property.PropertyType);

                    return (IQueryable<T>)method.Invoke(null, new object[] { query, lambda })!;
                }
            }

            // Apply default sort
            return request.SortDescending
                ? query.OrderByDescending(defaultSort)
                : query.OrderBy(defaultSort);
        }

        public static IQueryable<T> ApplyPaging<T>(IQueryable<T> query, PagedRequest request)
        {
            return query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);
        }

        private static Expression? BuildPropertySearchExpression(
            MemberExpression propertyAccess,
            Type propertyType,
            string searchTerm,
            string searchTermLower)
        {
            if (propertyType == typeof(string))
            {
                return BuildStringSearchExpression(propertyAccess, searchTermLower);
            }

            return BuildExactMatchExpression(propertyAccess, propertyType, searchTerm);
        }

        private static Expression BuildStringSearchExpression(MemberExpression propertyAccess, string searchTermLower)
        {
            // String: case-insensitive contains
            var nullCheck = Expression.NotEqual(propertyAccess, Expression.Constant(null, typeof(string)));
            var toLower = Expression.Call(propertyAccess, typeof(string).GetMethod("ToLower", Type.EmptyTypes)!);
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
            var containsCall = Expression.Call(toLower, containsMethod, Expression.Constant(searchTermLower));
            return Expression.AndAlso(nullCheck, containsCall);
        }

        private static Expression? BuildExactMatchExpression(MemberExpression propertyAccess, Type propertyType, string searchTerm)
        {
            try
            {
                var underlyingType = Nullable.GetUnderlyingType(propertyType);
                var targetType = underlyingType ?? propertyType;
                var isNullable = underlyingType != null;

                var parsedValue = ParseValue(searchTerm, targetType);
                if (parsedValue == null) return null;

                var constant = Expression.Constant(parsedValue, targetType);

                if (isNullable)
                {
                    // For nullable types: property.HasValue && property.Value == parsedValue
                    var hasValue = Expression.Property(propertyAccess, "HasValue");
                    var value = Expression.Property(propertyAccess, "Value");
                    var equals = Expression.Equal(value, constant);
                    return Expression.AndAlso(hasValue, equals);
                }

                return Expression.Equal(propertyAccess, constant);
            }
            catch
            {
                return null;
            }
        }

        private static object? ParseValue(string searchTerm, Type targetType)
        {
            if (targetType == typeof(Guid) && Guid.TryParse(searchTerm, out var guidValue))
                return guidValue;
            if (targetType == typeof(int) && int.TryParse(searchTerm, out var intValue))
                return intValue;
            if (targetType == typeof(long) && long.TryParse(searchTerm, out var longValue))
                return longValue;
            if (targetType == typeof(decimal) && decimal.TryParse(searchTerm, out var decimalValue))
                return decimalValue;
            if (targetType == typeof(double) && double.TryParse(searchTerm, out var doubleValue))
                return doubleValue;
            if (targetType == typeof(float) && float.TryParse(searchTerm, out var floatValue))
                return floatValue;
            if (targetType == typeof(bool) && bool.TryParse(searchTerm, out var boolValue))
                return boolValue;
            if (targetType == typeof(DateTime) && DateTime.TryParse(searchTerm, out var dateValue))
                return dateValue;
            if (targetType == typeof(DateOnly) && DateOnly.TryParse(searchTerm, out var dateOnlyValue))
                return dateOnlyValue;
            if (targetType == typeof(TimeOnly) && TimeOnly.TryParse(searchTerm, out var timeOnlyValue))
                return timeOnlyValue;

            return null;
        }
    }
}
