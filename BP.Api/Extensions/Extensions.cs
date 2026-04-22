using BP.Application;
using BP.Application.Interfaces;
using BP.Application.Interfaces.Admin;
using BP.Application.Services;
using BP.Application.Services.Admin;
using BP.Infrastructure.Repositories;
using BP.Infrastructure.Repositories.Admin;
using FluentValidation.AspNetCore;

namespace BP.Api.Extensions;

public static class Extensions
{
    public static IServiceCollection AddExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Description = "BPlanner API Document",
                Title = "BPlanner API",
                Version = "v1",
            });
        });

        services.AddControllers().AddFluentValidation(fv =>
        {
            fv.RegisterValidatorsFromAssemblyContaining<Program>();
            fv.AutomaticValidationEnabled = true;
            fv.ImplicitlyValidateChildProperties = true;
        });

        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IAdminRepository, AdminRepository>();

        services.AddScoped<IOperationService, OperationService>();
        services.AddScoped<IOperationRepository, OperationRepository>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAccountRepository, AccountRepository>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtOperations, JwtOperations>();

        return services;
    }
}