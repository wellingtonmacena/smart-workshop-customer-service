using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartWorkshop.Core.Infrastructure.Persistence;
using SmartWorkshop.Core.Infrastructure.Repositories;
using SmartWorkshop.Shared.EventBus.MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(SmartWorkshop.Core.Application.Commands.CreatePerson.CreatePersonCommand).Assembly);
});

// Register FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(SmartWorkshop.Core.Application.Commands.CreatePerson.CreatePersonCommandValidator).Assembly);

// Configure Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? builder.Configuration.GetConnectionString("CoreDatabase")
    ?? "Host=localhost;Port=5432;Database=smart_workshop_core;Username=postgres;Password=postgres";

builder.Services.AddDbContext<CoreDbContext>(options =>
    options.UseNpgsql(connectionString));

// Register Repositories
builder.Services.AddScoped<PersonRepository>();
builder.Services.AddScoped<VehicleRepository>();
builder.Services.AddScoped<SupplyRepository>();
builder.Services.AddScoped<AvailableServiceRepository>();

// Register MassTransit with RabbitMQ
builder.Services.AddMassTransitWithRabbitMQ(builder.Configuration);

// Add Health Checks
builder.Services.AddHealthChecks();

// CORS
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

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
