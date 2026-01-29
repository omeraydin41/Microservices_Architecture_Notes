using MediatR;
using Microservices.Catalog.Api.Features.Categories;
using Microservices.Catalog.Api.Features.Categories.Create;
using Microservices.Catalog.Api.Options;
using Microservices.Catalog.Api.Repostories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//builder.Services.AddOptions<MongoOption>().BindConfiguration(nameof(MongoOption)).ValidateDataAnnotations().ValidateOnStart();
//üst satýr yerýne Options klasoru >OptionExt clasýndan  AppOptionExt methodunu kullandýk . ütteki yorum satýrýný taþýdýðýmýz class. 
builder.Services.AppOptionExt();


//OPTÝONS ÝÇÝ RepostoryExt STATIC CLASININ EXTENCÝON AppDatabaseServiceExt METHODUNU KULLANDIK
builder.Services.AppDatabaseServiceExt();



var app = builder.Build();

app.AddCategoryGroupEndpointExt();
//AddCategoryGroupEndpointExt methodu CategoryEndpointExt clasýndan gelen guruplama methodudur ve bu method CreateCategoryEndpoint
//CreateCategoryEndpoint clasýndaký MÝNÝMAL APÝLERÝ guruplandýrarak tek merkezden yöenilmesini saðlar 







// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.Run();


