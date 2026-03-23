using Microservice.Discount.Api;
using Microservice.Discount.Api.Options;
using Microsoft.AspNetCore.Builder;
using UdemyNewMicroservice.Discount.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// --- SERVÝS YAPILANDIRMASI ---
// API keþif desteðini ekle (Minimal API veya Controller fark etmeksizin)
builder.Services.AddEndpointsApiExplorer();//swaggerin oluþmasý için 

//builder.Services.AddOpenApi();

// .NET 9 Yerleþik OpenAPI desteði
//builder.Services.AddOpenApi();

// Klasik Swagger UI desteði için servis
builder.Services.AddSwaggerGen();

builder.Services.AddVersioningExt();
builder.Services.AddCommonServiceExt(typeof(DiscountAssembly));
builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();



var app = builder.Build();

// --- PIPELINE (ARA KATMAN) YAPILANDIRMASI ---
if (app.Environment.IsDevelopment())
{
    // .NET 9'un yeni OpenAPI endpoint'ini aktif eder (/openapi/v1.json)
    app.MapOpenApi();

    // Görsel Swagger arayüzünü aktif eder (/swagger)
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); 
app.Run();