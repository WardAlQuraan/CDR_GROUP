using AutoMapper;
using cdr_group.Contracts.DTOs.ContactUs;
using cdr_group.Contracts.DTOs.Company;
using cdr_group.Contracts.DTOs.CompanyContact;
using cdr_group.Contracts.DTOs.CompanyBackground;
using cdr_group.Contracts.DTOs.Employee;
using cdr_group.Contracts.DTOs.Event;
using cdr_group.Contracts.DTOs.FileAttachment;
using cdr_group.Contracts.DTOs.Identity;
using cdr_group.Contracts.DTOs.Position;
using cdr_group.Contracts.DTOs.AuditLog;
using cdr_group.Contracts.DTOs.Complaint;
using cdr_group.Contracts.DTOs.Review;
using cdr_group.Contracts.DTOs.SalaryHistory;
using cdr_group.Contracts.DTOs.Country;
using cdr_group.Contracts.DTOs.City;
using cdr_group.Contracts.DTOs.Partner;
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
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src =>
                    src.Company != null ? src.Company.NameEn : null))
                .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src =>
                    src.Position != null ? src.Position.NameEn : null));

            CreateMap<Employee, EmployeeBasicDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src =>
                    src.Company != null ? src.Company.NameEn : null))
                .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src =>
                    src.Position != null ? src.Position.NameEn : null));

            CreateMap<Employee, EmployeeWithSubordinatesDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src =>
                    src.User != null ? src.User.Username : null))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src =>
                    src.Company != null ? src.Company.NameEn : null))
                .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src =>
                    src.Position != null ? src.Position.NameEn : null))
                .ForMember(dest => dest.Subordinates, opt => opt.MapFrom(src =>
                    src.Subordinates.Where(s => !s.IsDeleted).ToList()));

            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

            CreateMap<UpdateEmployeeDto, Employee>()
                .ForMember(dest => dest.SalaryHistories, opt => opt.Ignore())
                .ForSourceMember(src => src.SalaryChangeReason, opt => opt.DoNotValidate());
            // Company mappings
            CreateMap<Company, CompanyDto>()
                .ForMember(dest => dest.ParentNameEn, opt => opt.MapFrom(src =>
                    src.Parent != null ? src.Parent.NameEn : null))
                .ForMember(dest => dest.ParentNameAr, opt => opt.MapFrom(src =>
                    src.Parent != null ? src.Parent.NameAr : null))
                .ForMember(dest => dest.PartnersCount, opt => opt.Ignore());

            CreateMap<Company, CompanyBasicDto>();

            CreateMap<CreateCompanyDto, Company>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

            CreateMap<UpdateCompanyDto, Company>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Position mappings
            CreateMap<Position, PositionDto>();

            CreateMap<Position, PositionBasicDto>();

            CreateMap<Position, PositionWithEmployeesDto>();

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
                    src.Company != null ? src.Company.NameAr : null));

            CreateMap<CreateEventDto, Event>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

            CreateMap<UpdateEventDto, Event>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // ContactUs mappings
            CreateMap<ContactUs, ContactUsDto>()
                .ForMember(dest => dest.CompanyNameEn, opt => opt.MapFrom(src =>
                    src.Company != null ? src.Company.NameEn : null))
                .ForMember(dest => dest.CompanyNameAr, opt => opt.MapFrom(src =>
                    src.Company != null ? src.Company.NameAr : null));

            CreateMap<CreateContactUsDto, ContactUs>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

            CreateMap<UpdateContactUsDto, ContactUs>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // SalaryHistory mappings
            CreateMap<SalaryHistory, SalaryHistoryDto>()
                .ForMember(dest => dest.EmployeeNameEn, opt => opt.MapFrom(src =>
                    src.Employee != null ? $"{src.Employee.FirstNameEn} {src.Employee.LastNameEn}" : null))
                .ForMember(dest => dest.EmployeeNameAr, opt => opt.MapFrom(src =>
                    src.Employee != null ? $"{src.Employee.FirstNameAr} {src.Employee.LastNameAr}" : null));

            CreateMap<CreateSalaryHistoryDto, SalaryHistory>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

            CreateMap<UpdateSalaryHistoryDto, SalaryHistory>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // CompanyContact mappings
            CreateMap<CompanyContact, CompanyContactDto>()
                .ForMember(dest => dest.CompanyNameEn, opt => opt.MapFrom(src =>
                    src.Company != null ? src.Company.NameEn : null))
                .ForMember(dest => dest.CompanyNameAr, opt => opt.MapFrom(src =>
                    src.Company != null ? src.Company.NameAr : null));

            CreateMap<CreateCompanyContactDto, CompanyContact>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

            CreateMap<UpdateCompanyContactDto, CompanyContact>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // CompanyBackground mappings
            CreateMap<CompanyBackground, CompanyBackgroundDto>()
                .ForMember(dest => dest.CompanyNameEn, opt => opt.MapFrom(src =>
                    src.Company != null ? src.Company.NameEn : null))
                .ForMember(dest => dest.CompanyNameAr, opt => opt.MapFrom(src =>
                    src.Company != null ? src.Company.NameAr : null));

            CreateMap<CreateCompanyBackgroundDto, CompanyBackground>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

            CreateMap<UpdateCompanyBackgroundDto, CompanyBackground>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // AuditLog mappings
            CreateMap<AuditLog, AuditLogDto>();

            // Review mappings
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.CompanyNameEn, opt => opt.MapFrom(src =>
                    src.Company != null ? src.Company.NameEn : null))
                .ForMember(dest => dest.CompanyNameAr, opt => opt.MapFrom(src =>
                    src.Company != null ? src.Company.NameAr : null));

            CreateMap<CreateReviewDto, Review>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

            CreateMap<UpdateReviewDto, Review>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Complaint mappings
            CreateMap<Complaint, ComplaintDto>()
                .ForMember(dest => dest.CompanyNameEn, opt => opt.MapFrom(src =>
                    src.Company != null ? src.Company.NameEn : null))
                .ForMember(dest => dest.CompanyNameAr, opt => opt.MapFrom(src =>
                    src.Company != null ? src.Company.NameAr : null));

            CreateMap<CreateComplaintDto, Complaint>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

            CreateMap<UpdateComplaintDto, Complaint>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Country mappings
            CreateMap<Country, CountryDto>();

            CreateMap<CreateCountryDto, Country>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

            CreateMap<UpdateCountryDto, Country>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // City mappings
            CreateMap<City, CityDto>()
                .ForMember(dest => dest.CountryNameEn, opt => opt.MapFrom(src =>
                    src.Country != null ? src.Country.NameEn : null))
                .ForMember(dest => dest.CountryNameAr, opt => opt.MapFrom(src =>
                    src.Country != null ? src.Country.NameAr : null));

            CreateMap<CreateCityDto, City>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

            CreateMap<UpdateCityDto, City>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // Partner mappings
            CreateMap<Partner, PartnerDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.CompanyNameEn, opt => opt.MapFrom(src =>
                    src.Company != null ? src.Company.NameEn : null))
                .ForMember(dest => dest.CompanyNameAr, opt => opt.MapFrom(src =>
                    src.Company != null ? src.Company.NameAr : null))
                .ForMember(dest => dest.CityNameEn, opt => opt.MapFrom(src =>
                    src.City != null ? src.City.NameEn : null))
                .ForMember(dest => dest.CityNameAr, opt => opt.MapFrom(src =>
                    src.City != null ? src.City.NameAr : null))
                .ForMember(dest => dest.CityLatitude, opt => opt.MapFrom(src =>
                    src.City != null ? (double?)src.City.Latitude : null))
                .ForMember(dest => dest.CityLongitude, opt => opt.MapFrom(src =>
                    src.City != null ? (double?)src.City.Longitude : null));

            CreateMap<CreatePartnerDto, Partner>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                    Enum.Parse<Domain.Enums.PartnerStatus>(src.Status, true)));

            CreateMap<UpdatePartnerDto, Partner>()
                .ForMember(dest => dest.Status, opt =>
                {
                    opt.PreCondition(src => src.Status != null);
                    opt.MapFrom(src => Enum.Parse<Domain.Enums.PartnerStatus>(src.Status!, true));
                })
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
