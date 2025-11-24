using Microsoft.EntityFrameworkCore;
using Saturday_Back;
using Saturday_Back.Dtos;
using Saturday_Back.Entities;
using Saturday_Back.Repositories;
using Saturday_Back.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FssDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DevelopmentDb"),
        new MySqlServerVersion(new Version(8, 0, 44)
    )));


builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddMemoryCache();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<ApplicationMappingProfile>();
});

builder.Services.AddScoped<ScheduleService>();
builder.Services.AddCachedRepoWithService<BaseCost, BaseCostResponseDto, BaseCostService>(
    cacheKey: "BaseCostsCache");
builder.Services.AddCachedRepoWithService<BenefitType, BenefitTypeResponseDto, BenefitTypeService>(
    cacheKey: "BenefitTypeCache");
builder.Services.AddCachedRepoWithService<Subject, SubjectResponseDto, SubjectsService>(
    cacheKey: "SubjectCache");
builder.Services.AddCachedRepoWithService<PaymentType, PaymentTypeResponseDto, PaymentTypeService>(
    cacheKey: "PaymentTypeCache");


builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();

