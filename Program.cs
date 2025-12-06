using Microsoft.EntityFrameworkCore;
using Saturday_Back.Common.Database;
using Saturday_Back.Common.Repositories;
using Saturday_Back.Features.AcademicYears;
using Saturday_Back.Features.BenefitTypes;
using Saturday_Back.Features.PaymentTypes;
using Saturday_Back.Features.Subjects;
using Saturday_Back.Features.Schedules;
using Saturday_Back.Features.Students;
using Scalar.AspNetCore;
using Saturday_Back.Common;
using Microsoft.AspNetCore.Mvc;
using Saturday_Back.Common.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<FssDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DevelopmentDb"),
        new MySqlServerVersion(new Version(8, 0, 44)
    )));

// Generic repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Controllers
builder.Services.AddControllers();

// CORS
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

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Caching
builder.Services.AddMemoryCache();

// AutoMapper - automatically discovers all Profile classes in the assembly
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<ApplicationMappingProfile>();
});

// Register cached repositories with services (for reference/lookup data)
builder.Services.AddCachedRepoWithService<PaymentType, PaymentTypeService>(
    cacheKey: "PaymentTypesCache");
builder.Services.AddCachedRepoWithService<BenefitType, BenefitTypeService>(
    cacheKey: "BenefitTypesCache");
builder.Services.AddCachedRepoWithService<Subject, SubjectService>(
    cacheKey: "SubjectsCache");
builder.Services.AddCachedRepoWithService<AcademicYear, AcademicYearService>(
    cacheKey: "AcademicYearsCache");

// Register non-cached repository services (for transactional data)
builder.Services.AddScoped<StudentService>();

// Register schedule-related services
builder.Services.AddScoped<ScheduleLookupService>();
builder.Services.AddScoped<ScheduleEntriesGenerator>();
builder.Services.AddScoped<ScheduleService>();

// OpenAPI/Swagger
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
