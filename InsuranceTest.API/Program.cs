using System.Text.Json;
using InsuranceTest.Data.DataAccess;
using InsuranceTest.Data.DataAccess.Interfaces;
using InsuranceTest.Service.Managers;
using InsuranceTest.Service.Managers.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repository
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IClaimRepository, ClaimRepository>();

// Managers
builder.Services.AddScoped<ICompanyManager, CompanyManager>();
builder.Services.AddScoped<IClaimsManager, ClaimsManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();