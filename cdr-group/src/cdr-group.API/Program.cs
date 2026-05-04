using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using cdr_group.Application.Mappings;
using cdr_group.Domain.Localization;
using cdr_group.Application.Services;
using cdr_group.Application.Settings;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Infrastructure.Authorization;
using cdr_group.Infrastructure.Middleware;
using cdr_group.Infrastructure.Services;
using cdr_group.Persistence.Data;
using cdr_group.Persistence.Repositories;
using cdr_group.Persistence.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// JWT Settings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>()!;

// Database context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var serverVersion = ServerVersion.AutoDetect(connectionString);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, serverVersion));

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = async context =>
        {
            context.HandleResponse();

            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";

                var language = context.Request.Headers["Accept-Language"].FirstOrDefault() ?? "en";
                var isExpired = context.AuthenticateFailure is SecurityTokenExpiredException;
                var messageKey = isExpired ? Messages.SessionExpired : Messages.Unauthorized;

                var response = new
                {
                    success = false,
                    data = (object?)null,
                    message = Messages.Get(messageKey, language),
                    errors = Array.Empty<string>()
                };

                var json = System.Text.Json.JsonSerializer.Serialize(response, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                });
                await context.Response.WriteAsync(json);
            }
        },
        OnForbidden = async context =>
        {
            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = 403;
                context.Response.ContentType = "application/json";

                var language = context.Request.Headers["Accept-Language"].FirstOrDefault() ?? "en";
                var response = new
                {
                    success = false,
                    data = (object?)null,
                    message = Messages.Get(Messages.Forbidden, language),
                    errors = Array.Empty<string>()
                };

                var json = System.Text.Json.JsonSerializer.Serialize(response, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                });
                await context.Response.WriteAsync(json);
            }
        }
    };
});

// Authorization
builder.Services.AddAuthorization();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

// HttpContext accessor for current user service
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

// Repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IFileAttachmentRepository, FileAttachmentRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IContactUsRepository, ContactUsRepository>();
builder.Services.AddScoped<ISalaryHistoryRepository, SalaryHistoryRepository>();
builder.Services.AddScoped<ICompanyContactRepository, CompanyContactRepository>();
builder.Services.AddScoped<ICompanyBackgroundRepository, CompanyBackgroundRepository>();
builder.Services.AddScoped<ICompanyFormRepository, CompanyFormRepository>();
builder.Services.AddScoped<ICompanyPreferenceRepository, CompanyPreferenceRepository>();
builder.Services.AddScoped<ICompanyBranchRepository, CompanyBranchRepository>();
builder.Services.AddScoped<ICompanyFinancialClausesRightsRepository, CompanyFinancialClausesRightsRepository>();
builder.Services.AddScoped<ICompanyClientReachRepository, CompanyClientReachRepository>();
builder.Services.AddScoped<ICompanyTitleDescriptionRepository, CompanyTitleDescriptionRepository>();
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IComplaintRepository, ComplaintRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IPartnerRepository, PartnerRepository>();

// Services
builder.Services.AddScoped<IAuditDisplayNameResolver, AuditDisplayNameResolver>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IFileAttachmentService, FileAttachmentService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IContactUsService, ContactUsService>();
builder.Services.AddScoped<ISalaryHistoryService, SalaryHistoryService>();
builder.Services.AddScoped<ICompanyContactService, CompanyContactService>();
builder.Services.AddScoped<ICompanyBackgroundService, CompanyBackgroundService>();
builder.Services.AddScoped<ICompanyFormService, CompanyFormService>();
builder.Services.AddScoped<ICompanyPreferenceService, CompanyPreferenceService>();
builder.Services.AddScoped<ICompanyBranchService, CompanyBranchService>();
builder.Services.AddScoped<ICompanyFinancialClausesRightsService, CompanyFinancialClausesRightsService>();
builder.Services.AddScoped<ICompanyClientReachService, CompanyClientReachService>();
builder.Services.AddScoped<ICompanyTitleDescriptionService, CompanyTitleDescriptionService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IComplaintService, ComplaintService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IPartnerService, PartnerService>();
builder.Services.AddScoped<IExcelExportService, ExcelExportService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CDR Group API",
        Version = "v1",
        Description = "CDR Group API with JWT Authentication"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token"
    });

    options.AddSecurityRequirement(_ => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer"),
            new List<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200", "https://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});
var app = builder.Build();

// Auto-migrate database on startup (creates DB if not exists, applies pending migrations)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
app.UseExceptionHandlerMiddleware();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS must be before Authentication/Authorization
app.UseCors("AllowAngular");

// Serve static files from wwwroot (Angular build)
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseActiveUserCheck();
app.UseAuthorization();

app.MapControllers();

// SPA fallback - for Angular routing, redirect non-API requests to index.html
app.MapFallbackToFile("index.html");

app.Run();
