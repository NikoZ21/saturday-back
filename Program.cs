using Microsoft.EntityFrameworkCore;
using Saturday_Back;
using Saturday_Back.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


// Configure DbContext with connection string from appsettings.json
builder.Services.AddDbContext<FssDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DevelopmentDb"),
        new MySqlServerVersion(new Version(8, 0, 44)
    )));

//Register controllers
builder.Services.AddControllers();

// Register IMemoryCache and PaymentService (singleton)
builder.Services.AddMemoryCache();
builder.Services.AddScoped<PaymentTypeService>();
builder.Services.AddScoped<BenefitTypeService>();


//Open API / Scalar API
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

