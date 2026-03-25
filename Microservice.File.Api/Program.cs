using Microservice.File.Api;
using NewMicroservices.Shared.Extansions;

var builder = WebApplication.CreateBuilder(args);

// Swagger ve API Explorer servisleri eklnedi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCommonServiceExt(typeof(FileAssembly));
builder.Services.AddVersioningExt();

var app = builder.Build();

// Geliþtirme ortamýnda Swagger'ý aktif edildi
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}
app.UseHttpsRedirection();
app.Run();