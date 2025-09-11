using BP.Api.Extensions;
using BP.Application;
using BP.Application.Interfaces;
using BP.Application.Interfaces.Admin;
using BP.Application.Services;
using BP.Application.Services.Admin;
using BP.Infrastructure;
using BP.Infrastructure.Interfaces;
using BP.Infrastructure.Interfaces.Admin;
using BP.Infrastructure.Repositories;
using BP.Infrastructure.Repositories.Admin;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Description = "BPlanner API Document",
        Title = "BPlanner API",
        Version = "v1",
    });
});

var context = new CustomAssemblyLoadContext();
context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));

builder.Services.AddControllers().AddFluentValidation(
    fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<Program>();
        fv.AutomaticValidationEnabled = true;
        fv.ImplicitlyValidateChildProperties = true;
    });

builder.Services.AddScoped<IOperationService, OperationService>();
builder.Services.AddScoped<IOperationRepository, OperationRepository>();

builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddScoped<IDocumentService, DocumentService>();

var connectionString = builder.Configuration.GetSection("ConnectionString");
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddScoped<PasswordHasher>();
builder.Services.AddScoped<JwtOperations>();

builder.Services.AddDbContext<BPlannerDbContext>(
    options =>
    {
        options.UseNpgsql(connectionString.Value);
    });

builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JwtOptions:Issuer"],           
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JwtOptions:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:Key"])),
            ValidateLifetime = true
        };

        options.Events = new()
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["bp"];
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseReDoc(options =>
{
    options.DocumentTitle = "BPlanner API Document";
    options.SpecUrl = "/swagger/v1/swagger.json";
    options.RoutePrefix = "api-document";
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");

app.Run();
