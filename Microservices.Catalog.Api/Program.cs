using MediatR;
using Microservices.Catalog.Api;
using Microservices.Catalog.Api.Features.Categories;
using Microservices.Catalog.Api.Features.Categories.Create;
using Microservices.Catalog.Api.Options;
using Microservices.Catalog.Api.Repostories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NewMicroservices.Shared.Extansions;

var builder = WebApplication.CreateBuilder(args);

// Klasik Swagger Servislerini Ekle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddOptions<MongoOption>().BindConfiguration(nameof(MongoOption)).ValidateDataAnnotations().ValidateOnStart();
//üst satýr yerýne Options klasoru >OptionExt clasýndan  AppOptionExt methodunu kullandýk . ütteki yorum satýrýný taþýdýðýmýz class. 
builder.Services.AppOptionExt();


//OPTÝONS ÝÇÝ RepostoryExt STATIC CLASININ EXTENCÝON AppDatabaseServiceExt METHODUNU KULLANDIK
builder.Services.AppDatabaseServiceExt();


// shared > extansýons > CommonServiceExt clasýndan gelen AddCommonServiceExt methodu na assembly clasýmýz olan catalog api altýnaký CatalogAssembly classý veridi
builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));


var app = builder.Build();

app.AddCategoryGroupEndpointExt();
//AddCategoryGroupEndpointExt methodu CategoryEndpointExt clasýndan gelen guruplama methodudur ve bu method CreateCategoryEndpoint
//CreateCategoryEndpoint clasýndaký MÝNÝMAL APÝLERÝ guruplandýrarak tek merkezden yöenilmesini saðlar 


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   // Swagger dökümanýný oluþturur
    app.UseSwaggerUI(); // Yeþil renkli klasik arayüzü açar
}


app.Run();