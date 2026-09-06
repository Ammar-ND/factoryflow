using FactoryFlow.Api.ExceptionHandling;
using FactoryFlow.Application;
using FactoryFlow.Application.Abstractions.Persistence;
using FactoryFlow.Infrastructure.Persistence;
using FactoryFlow.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();

builder.Services.AddDbContext<FactoryFlowDbContext>(options =>
{
    var connectionString =
        builder.Configuration.GetConnectionString("Database");

    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<
    IProductionOrderRepository,
    ProductionOrderRepository>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IUnitOfWork>(serviceProvider =>
    serviceProvider.GetRequiredService<FactoryFlowDbContext>());

builder.Services.AddScoped<IMachineRepository, MachineRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


builder.Services.AddOpenApi();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("Frontend");

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

app.Run();
