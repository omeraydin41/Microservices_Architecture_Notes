
using Microservices.Basket.Api;
using NewMicroservices.Shared.Extansions;
public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddCommonServiceExt(typeof(BasketAssembly));//mapper validaston ve mediatR hepsi hazýr geldi.

        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis");
        });//bundan sonra IDistributedCache üzerinden redis cache'e eriþebiliriz.



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }


        app.Run();
    }
}