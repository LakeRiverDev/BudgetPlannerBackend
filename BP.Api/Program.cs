using BP.Api.Extensions;
using BP.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BP.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExtensions(builder.Configuration);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
var connectionString = builder.Configuration.GetSection("ConnectionString");

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
        policy.SetIsOriginAllowed(s=>true)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
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

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");

app.Run();
