using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using cdr_group.API.Controllers.Base;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.FileAttachment;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Infrastructure.Authorization;

namespace cdr_group.API.Controllers
{
    public class FilesController : BaseController<FileAttachmentDto, CreateFileAttachmentDto, UpdateFileAttachmentDto, IFileAttachmentService>
    {
        protected override string EntityName => "File";

        public FilesController(IFileAttachmentService fileAttachmentService) : base(fileAttachmentService)
        {
        }

        [HttpGet]
        public override async Task<ActionResult<ApiResponse<PagedResult<FileAttachmentDto>>>> GetPaged([FromQuery] PagedRequest request)
        {
            return await base.GetPaged(request);
        }

        [HttpGet("{id:guid}")]
        public override async Task<ActionResult<ApiResponse<FileAttachmentDto>>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("by-entity/{entityId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<IEnumerable<FileAttachmentDto>>>> GetByEntityId(Guid entityId)
        {
            var files = await Service.GetByEntityIdAsync(entityId);
            return Ok(ApiResponse<IEnumerable<FileAttachmentDto>>.SuccessResponse(files));
        }
        [AllowAnonymous]

        [HttpGet("by-type/{entityType}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<FileAttachmentDto>>>> GetByEntityType(string entityType)
        {
            var files = await Service.GetByEntityTypeAsync(entityType);
            return Ok(ApiResponse<IEnumerable<FileAttachmentDto>>.SuccessResponse(files));
        }

        [HttpGet("by-entity/{entityId:guid}/{entityType}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<FileAttachmentDto>>>> GetByEntity(Guid entityId, string entityType)
        {
            var files = await Service.GetByEntityAsync(entityId, entityType);
            return Ok(ApiResponse<IEnumerable<FileAttachmentDto>>.SuccessResponse(files));
        }

        [HttpPost]
        [HasPermission(Permissions.Files.Upload)]
        public async Task<ActionResult<ApiResponse<FileAttachmentDto>>> Upload([FromForm] CreateFileAttachmentDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
            {
                return BadRequest(ApiResponse<FileAttachmentDto>.FailureResponse("No file uploaded."));
            }

            var file = await Service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = file.Id }, ApiResponse<FileAttachmentDto>.SuccessResponse(file, "File uploaded successfully."));
        }

        // Hide base Create method - use Upload instead for file uploads
        [NonAction]
        public override Task<ActionResult<ApiResponse<FileAttachmentDto>>> Create([FromBody] CreateFileAttachmentDto dto)
        {
            throw new NotSupportedException("Use Upload endpoint instead.");
        }

        [HttpPut("{id:guid}")]
        [HasPermission(Permissions.Files.Update)]
        public override async Task<ActionResult<ApiResponse<FileAttachmentDto>>> Update(Guid id, [FromBody] UpdateFileAttachmentDto dto)
        {
            return await base.Update(id, dto);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.Files.Delete)]
        public override async Task<ActionResult<ApiResponse>> Delete(Guid id)
        {
            return await base.Delete(id);
        }

        [HttpPost("bulk")]
        [HasPermission(Permissions.Files.Upload)]
        public async Task<ActionResult<ApiResponse<IEnumerable<BulkFileOperationResultDto>>>> BulkOperation([FromForm] List<BulkFileOperationItemDto> items)
        {
            if (items == null || items.Count == 0)
            {
                return BadRequest(ApiResponse<IEnumerable<BulkFileOperationResultDto>>.FailureResponse("No items provided."));
            }

            var results = await Service.BulkOperationAsync(items);
            return Ok(ApiResponse<IEnumerable<BulkFileOperationResultDto>>.SuccessResponse(results, "Bulk operation completed."));
        }
    }
}
