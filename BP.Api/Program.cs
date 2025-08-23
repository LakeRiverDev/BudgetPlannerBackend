using BP.Application.Interfaces;
using BP.Application.Interfaces.Admin;
using BP.Application.Services;
using BP.Application.Services.Admin;
using BP.DataBase;
using BP.DataBase.Interfaces;
using BP.DataBase.Interfaces.Admin;
using BP.DataBase.Repositories;
using BP.DataBase.Repositories.Admin;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IOperationService, OperationService>();
builder.Services.AddScoped<IOperationRepository, OperationRepository>();

builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();

var connectionString = builder.Configuration.GetSection("ConnectionString");
builder.Services.AddDbContext<BPlannerDbContext>(
    options =>
    {
        options.UseNpgsql(connectionString.Value);
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
