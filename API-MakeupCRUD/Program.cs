using API_MakeupCRUD.Automappers;
using API_MakeupCRUD.Context;
using API_MakeupCRUD.DTOs;
using API_MakeupCRUD.Models;
using API_MakeupCRUD.Repository;
using API_MakeupCRUD.Services;
using API_MakeupCRUD.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Entity Framework
var connectionString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// Services
builder.Services.AddScoped<IMakeupService, MakeupService>();

// Validator
builder.Services.AddScoped<IValidator<MakeupInsertDto>, MakeupInsertValidator>();
builder.Services.AddScoped<IValidator<MakeupUpdateDto>, MakeupUpdateValidator>();

// Repository
builder.Services.AddScoped<IRepository<MakeupProduct>, MakeupRepository>();

// Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
