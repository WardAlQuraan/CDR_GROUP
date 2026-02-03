using cdr_group.Contracts.DTOs.Common;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface IBaseService<TDto, TCreateDto, TUpdateDto>
        where TDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<PagedResult<TDto>> GetPagedAsync(PagedRequest request);
        Task<TDto?> GetByIdAsync(Guid id);
        Task<TDto> CreateAsync(TCreateDto dto);
        Task<TDto?> UpdateAsync(Guid id, TUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}
