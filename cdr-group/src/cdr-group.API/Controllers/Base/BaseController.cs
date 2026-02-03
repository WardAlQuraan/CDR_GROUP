using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Position;
using cdr_group.Contracts.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static cdr_group.Domain.Constants.Permissions;

namespace cdr_group.API.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public abstract class BaseController<TDto, TCreateDto, TUpdateDto, TService> : ControllerBase
        where TDto : class
        where TCreateDto : class
        where TUpdateDto : class
        where TService : IBaseService<TDto, TCreateDto, TUpdateDto>
    {
        protected readonly TService Service;
        protected abstract string EntityName { get; }

        protected BaseController(TService service)
        {
            Service = service;
        }

        [HttpGet]
        public virtual async Task<ActionResult<ApiResponse<PagedResult<TDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            var result = await Service.GetPagedAsync(request);
            return Ok(ApiResponse<PagedResult<TDto>>.SuccessResponse(result));
        }

        [HttpGet("all")]
        public virtual async Task<ActionResult<ApiResponse<IEnumerable<TDto>>>> GetAll()
        {
            var result = await Service.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<TDto>>.SuccessResponse(result));
        }

        [HttpGet("{id:guid}")]
        public virtual async Task<ActionResult<ApiResponse<TDto>>> GetById(Guid id)
        {
            var entity = await Service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound(ApiResponse<TDto>.FailureResponse($"{EntityName} not found."));
            }
            return Ok(ApiResponse<TDto>.SuccessResponse(entity));
        }

        [HttpPost]
        public virtual async Task<ActionResult<ApiResponse<TDto>>> Create([FromBody] TCreateDto dto)
        {
            var entity = await Service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = GetEntityId(entity) }, ApiResponse<TDto>.SuccessResponse(entity, $"{EntityName} created successfully."));
        }

        [HttpPut("{id:guid}")]
        public virtual async Task<ActionResult<ApiResponse<TDto>>> Update(Guid id, [FromBody] TUpdateDto dto)
        {
            var entity = await Service.UpdateAsync(id, dto);
            if (entity == null)
            {
                return NotFound(ApiResponse<TDto>.FailureResponse($"{EntityName} not found."));
            }
            return Ok(ApiResponse<TDto>.SuccessResponse(entity, $"{EntityName} updated successfully."));
        }

        [HttpDelete("{id:guid}")]
        public virtual async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            var result = await Service.DeleteAsync(id);
            if (!result)
            {
                return NotFound(ApiResponse.FailureResponse($"{EntityName} not found."));
            }
            return Ok(ApiResponse.SuccessResponse($"{EntityName} deleted successfully."));
        }

        [HttpGet("{id:guid}/exists")]
        public virtual async Task<ActionResult<ApiResponse<bool>>> Exists(Guid id)
        {
            var exists = await Service.ExistsAsync(id);
            return Ok(ApiResponse<bool>.SuccessResponse(exists));
        }

        protected virtual Guid GetEntityId(TDto entity)
        {
            var idProperty = typeof(TDto).GetProperty("Id");
            if (idProperty != null && idProperty.PropertyType == typeof(Guid))
            {
                return (Guid)idProperty.GetValue(entity)!;
            }
            throw new InvalidOperationException($"Entity {typeof(TDto).Name} does not have a Guid Id property.");
        }
    }
}
