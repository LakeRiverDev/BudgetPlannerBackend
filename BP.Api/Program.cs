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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IOperationService, OperationService>();
builder.Services.AddScoped<IOperationRepository, OperationRepository>();

builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var connectionString = builder.Configuration.GetSection("ConnectionString");
var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();

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
            ValidIssuer = jwtOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = jwtOptions.AUDIENCE,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.KEY)),
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
