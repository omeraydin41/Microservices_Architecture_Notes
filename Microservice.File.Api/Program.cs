using Microservice.File.Api;
using Microsoft.Extensions.FileProviders;
using NewMicroservices.Shared.Extansions;

var builder = WebApplication.CreateBuilder(args);

// Swagger ve API Explorer servisleri eklnedi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCommonServiceExt(typeof(FileAssembly));
builder.Services.AddVersioningExt();

//wwwroot için aramanýn yapýlacaðý yerin belirlenmesi

builder.Services.AddSingleton<IFileProvider>//IFileProvider : klasorlere eriþme Ýnterface
(new PhysicalFileProvider//Fiziksel býr sýnýf uzerinden alýnacak
(Path.Combine(Directory.GetCurrentDirectory()// dosyalardan Microservice.File.Api klasorune eriþtik 
, "wwwroot")));//Microservice.File.Api içinden wwwroot klasorune eriþtik


var app = builder.Build();

app.UseStaticFiles();

// Geliþtirme ortamýnda Swagger'ý aktif edildi
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}
app.UseHttpsRedirection();
app.Run();