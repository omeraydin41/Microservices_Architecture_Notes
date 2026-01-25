using Microservices.Catalog.Api.Options;
using Microservices.Catalog.Api.Repostories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//builder.Services.AddOptions<MongoOption>().BindConfiguration(nameof(MongoOption)).ValidateDataAnnotations().ValidateOnStart();

//üst satýr yerýne Options klasoru >OptionExt clasýndan  AppOptionExt methodunu kullandýk . ütteki yorum satýrýný taþýdýðýmýz class. 
builder.Services.AppOptionExt();


#region
//APPDBCONTEXT Ý DI AKTARMA : APPDB CONTEXT ÝÇÝNDEKÝ YARDIMCI METHOD(CREATE) HER KULLANILDIÐINDA NESEN ALINMALI 
//SÝNGELTON OLARAK AYAÐA KALKACAK //IMongoClient interface verdik,buna karþýlýk IMongoClient nesen orneði verdik //nesneyý verdýðýmýz yer 
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var option = sp.GetRequiredService<MongoOption>();
    return new MongoClient(option.ConnectionString);
    //nedený ConnectionString 1 tanedir 1 kere okumak yeterli

});
//mongoya her baðlanmak ýstediðimizde geriye AppDbContextdoneceðiz
builder.Services.AddScoped(sp => 
{
    //dý contaýneri interface uzerýnden ekledik ust methodda burda geri alacaðýz 
    var mongoClient = sp.GetRequiredService<IMongoClient>();// GetRequiredService ile IMongoClient eriþtik 

    var option = sp.GetRequiredService<MongoOption>();//MongoOption ý da aldýk

    return AppDbContext.Create(mongoClient.GetDatabase(option.DatabaseName));//create extensýon methodumuzdur optionext içindeki
    //cerate ile alýnan database name appsettýngdev içindeki alan adýný kullanacak 
});
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.Run();


