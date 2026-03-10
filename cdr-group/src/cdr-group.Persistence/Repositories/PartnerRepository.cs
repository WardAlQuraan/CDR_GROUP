using Microsoft.EntityFrameworkCore;
using cdr_group.Contracts.DTOs.Partner;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Enums;
using cdr_group.Persistence.Data;
using cdr_group.Persistence.Helpers;

namespace cdr_group.Persistence.Repositories
{
    public class PartnerRepository : Repository<Partner>, IPartnerRepository
    {
        public PartnerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Partner>> GetAllAsync()
        {
            return await _dbSet.Include(e => e.Company).Include(e => e.City).ThenInclude(c=>c.Country).Where(e => !e.IsDeleted).ToListAsync();
        }

        public override async Task<Partner?> GetByIdAsync(Guid id)
        {
            return await _dbSet.Include(e => e.Company).Include(e => e.City).FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task<IEnumerable<Partner>> GetAllByCompanyCodeAsync(string companyCode)
        {
            return await _dbSet
                .Include(e => e.Company)
                .Include(e => e.City).ThenInclude(x=>x.Country)
                .Where(e => !e.IsDeleted && e.Company != null && e.Company.Code == companyCode)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Partner> Items, int TotalCount)> GetPartnersPagedAsync(PartnerPagedRequest request)
        {
            var query = _dbSet.Include(e => e.Company).Include(e => e.City).Where(e => !e.IsDeleted);

            if (request.CompanyId.HasValue)
            {
                query = query.Where(e => e.CompanyId == request.CompanyId.Value);
            }

            if (request.CityId.HasValue)
            {
                query = query.Where(e => e.CityId == request.CityId.Value);
            }

            if (request.CountryId.HasValue)
            {
                query = query.Where(e => e.City != null && e.City.CountryId == request.CountryId.Value);
            }

            if (!string.IsNullOrEmpty(request.Status) && Enum.TryParse<PartnerStatus>(request.Status, true, out var status))
            {
                query = query.Where(e => e.Status == status);
            }

            query = QueryHelper.ApplySearch(query, request);
            query = QueryHelper.ApplySort(query, request, e => e.CreatedAt);

            var totalCount = await query.CountAsync();
            var items = await QueryHelper.ApplyPaging(query, request).ToListAsync();

            return (items, totalCount);
        }
    }
}
