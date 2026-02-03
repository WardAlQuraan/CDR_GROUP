using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities.Base;

namespace cdr_group.Application.Services
{
    public abstract class BaseService<TEntity, TDto, TCreateDto, TUpdateDto> : IBaseService<TDto, TCreateDto, TUpdateDto>
        where TEntity : BaseEntity
        where TDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IMapper Mapper;

        protected BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        protected abstract IRepository<TEntity> Repository { get; }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await Repository.GetAllAsync();
            return Mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual async Task<PagedResult<TDto>> GetPagedAsync(PagedRequest request)
        {
            var (entities, totalCount) = await Repository.GetPagedAsync(request);
            var dtos = Mapper.Map<List<TDto>>(entities);
            return new PagedResult<TDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }

        public virtual async Task<TDto?> GetByIdAsync(Guid id)
        {
            var entity = await Repository.GetByIdAsync(id);
            return Mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto> CreateAsync(TCreateDto dto)
        {
            await ValidateCreateAsync(dto);

            await UnitOfWork.BeginTransactionAsync();
            try
            {
                var entity = Mapper.Map<TEntity>(dto);
                await BeforeCreateAsync(entity, dto);

                await Repository.AddAsync(entity);
                await UnitOfWork.SaveChangesAsync();

                await AfterCreateAsync(entity, dto);

                await UnitOfWork.CommitTransactionAsync();

                return await GetByIdAsync(entity.Id) ?? Mapper.Map<TDto>(entity);
            }
            catch
            {
                await UnitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public virtual async Task<TDto?> UpdateAsync(Guid id, TUpdateDto dto)
        {
            var entity = await Repository.GetByIdAsync(id);
            if (entity == null) return default;

            await ValidateUpdateAsync(id, dto, entity);

            await UnitOfWork.BeginTransactionAsync();
            try
            {
                Mapper.Map(dto, entity);
                await BeforeUpdateAsync(entity, dto);

                await Repository.UpdateAsync(entity);
                await UnitOfWork.SaveChangesAsync();

                await AfterUpdateAsync(entity, dto);

                await UnitOfWork.CommitTransactionAsync();

                return await GetByIdAsync(id);
            }
            catch
            {
                await UnitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await Repository.GetByIdAsync(id);
            if (entity == null) return false;

            await ValidateDeleteAsync(id, entity);

            await UnitOfWork.BeginTransactionAsync();
            try
            {
                await BeforeDeleteAsync(entity);

                await Repository.DeleteAsync(entity);
                await UnitOfWork.SaveChangesAsync();

                await AfterDeleteAsync(entity);

                await UnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch
            {
                await UnitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            return await Repository.ExistsAsync(id);
        }

        // Template methods for validation - override in derived classes
        protected virtual Task ValidateCreateAsync(TCreateDto dto) => Task.CompletedTask;
        protected virtual Task ValidateUpdateAsync(Guid id, TUpdateDto dto, TEntity entity) => Task.CompletedTask;
        protected virtual Task ValidateDeleteAsync(Guid id, TEntity entity) => Task.CompletedTask;

        // Template methods for lifecycle hooks - override in derived classes
        protected virtual Task BeforeCreateAsync(TEntity entity, TCreateDto dto) => Task.CompletedTask;
        protected virtual Task AfterCreateAsync(TEntity entity, TCreateDto dto) => Task.CompletedTask;
        protected virtual Task BeforeUpdateAsync(TEntity entity, TUpdateDto dto) => Task.CompletedTask;
        protected virtual Task AfterUpdateAsync(TEntity entity, TUpdateDto dto) => Task.CompletedTask;
        protected virtual Task BeforeDeleteAsync(TEntity entity) => Task.CompletedTask;
        protected virtual Task AfterDeleteAsync(TEntity entity) => Task.CompletedTask;
    }
}
