using AutoMapper;
using cdr_group.Contracts.DTOs.Company;
using cdr_group.Contracts.DTOs.Department;
using cdr_group.Contracts.DTOs.Employee;
using cdr_group.Contracts.DTOs.Event;
using cdr_group.Contracts.DTOs.FileAttachment;
using cdr_group.Contracts.DTOs.Identity;
using cdr_group.Contracts.DTOs.Position;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Entities.Identity;

namespace cdr_group.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src =>
                    src.UserRoles.Where(ur => !ur.IsDeleted).Select(ur => ur.Role.Name).ToList()));

            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.UserRoles, opt => opt.Ignore());

            CreateMap<UpdateUserDto, User>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Role mappings
            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src =>
                    src.RolePermissions.Where(rp => !rp.IsDeleted).Select(rp => rp.Permission.Name).ToList()));

            CreateMap<CreateRoleDto, Role>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.IsSystemRole, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.RolePermissions, opt => opt.Ignore());

            CreateMap<UpdateRoleDto, Role>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Permission mappings
            CreateMap<Permission, PermissionDto>();

            CreateMap<CreatePermissionDto, Permission>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

            CreateMap<UpdatePermissionDto, Permission>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Employee mappings
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src =>
                    src.User != null ? src.User.Username : null))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src =>
                    src.Department != null ? src.Department.NameEn : null))
                .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src =>
                    src.Position != null ? src.Position.NameEn : null));

            CreateMap<Employee, EmployeeBasicDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src =>
                    src.Department != null ? src.Department.NameEn : null))
                .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src =>
                    src.Position != null ? src.Position.NameEn : null));

            CreateMap<Employee, EmployeeWithSubordinatesDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src =>
                    src.User != null ? src.User.Username : null))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src =>
                    src.Department != null ? src.Department.NameEn : null))
                .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src =>
                    src.Position != null ? src.Position.NameEn : null))
                .ForMember(dest => dest.Subordinates, opt => opt.MapFrom(src =>
                    src.Subordinates.Where(s => !s.IsDeleted).ToList()));

            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

            CreateMap<UpdateEmployeeDto, Employee>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Company mappings
            CreateMap<Company, CompanyDto>();

            CreateMap<Company, CompanyBasicDto>();

            CreateMap<CreateCompanyDto, Company>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

            CreateMap<UpdateCompanyDto, Company>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Department mappings
            CreateMap<Department, DepartmentDto>()
                .ForMember(dest => dest.ManagerName, opt => 
                opt.MapFrom(src =>
                    src.Manager != null ? $"{src.Manager.FirstNameEn} {src.Manager.LastNameEn}" : null))
                .ForMember(dest => dest.ManagerNameAr, opt => 
                opt.MapFrom(src =>
                    src.Manager != null ? $"{src.Manager.FirstNameAr} {src.Manager.LastNameAr}" : null))
                .ForMember(dest => dest.CompanyName, opt => 
                opt.MapFrom(src =>
                    src.Company != null ? $"{src.Company.NameEn}" : null))
                .ForMember(dest => dest.CompanyNameAr, opt => 
                opt.MapFrom(src =>
                    src.Company != null ? $"{src.Company.NameAr}" : null))
                ;

            CreateMap<Department, DepartmentBasicDto>();

            CreateMap<Department, DepartmentWithSubDepartmentsDto>()
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src =>
                    src.Manager != null ? $"{src.Manager.FirstNameEn} {src.Manager.LastNameEn}" : null))
                .ForMember(dest => dest.SubDepartments, opt => opt.MapFrom(src =>
                    src.SubDepartments.Where(s => !s.IsDeleted).ToList()));

            CreateMap<CreateDepartmentDto, Department>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

            CreateMap<UpdateDepartmentDto, Department>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Position mappings
            CreateMap<Position, PositionDto>()
                .ForMember(dest => dest.DepartmentNameEn, opt => opt.MapFrom(src =>
                    src.Department != null ? src.Department.NameEn : null))
                .ForMember(dest => dest.DepartmentNameAr, opt => opt.MapFrom(src =>
                    src.Department != null ? src.Department.NameAr : null));

            CreateMap<Position, PositionBasicDto>();

            CreateMap<Position, PositionWithEmployeesDto>()
                .ForMember(dest => dest.DepartmentNameEn, opt => opt.MapFrom(src =>
                    src.Department != null ? src.Department.NameEn : null))
                .ForMember(dest => dest.DepartmentNameAr, opt => opt.MapFrom(src =>
                    src.Department != null ? src.Department.NameAr : null));

            CreateMap<CreatePositionDto, Position>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

            CreateMap<UpdatePositionDto, Position>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // FileAttachment mappings
            CreateMap<FileAttachment, FileAttachmentDto>();

            CreateMap<CreateFileAttachmentDto, FileAttachment>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.FileName, opt => opt.Ignore())
                .ForMember(dest => dest.StoredFileName, opt => opt.Ignore())
                .ForMember(dest => dest.Path, opt => opt.Ignore())
                .ForMember(dest => dest.ContentType, opt => opt.Ignore())
                .ForMember(dest => dest.Size, opt => opt.Ignore());

            CreateMap<UpdateFileAttachmentDto, FileAttachment>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Event mappings
            CreateMap<Event, EventDto>()
                .ForMember(dest => dest.CompanyNameEn, opt => opt.MapFrom(src =>
                    src.Company != null ? src.Company.NameEn : null))
                .ForMember(dest => dest.CompanyNameAr, opt => opt.MapFrom(src =>
                    src.Company != null ? src.Company.NameAr : null))
                .ForMember(dest => dest.DepartmentNameEn, opt => opt.MapFrom(src =>
                    src.Department != null ? src.Department.NameEn : null))
                .ForMember(dest => dest.DepartmentNameAr, opt => opt.MapFrom(src =>
                    src.Department != null ? src.Department.NameAr : null));

            CreateMap<CreateEventDto, Event>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

            CreateMap<UpdateEventDto, Event>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
