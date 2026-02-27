using Microservices.Basket.Api;
using Microservices.Basket.Api.Features.Baskets;
using NewMicroservices.Shared.Extansions;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddOpenApi();

        // --- Swagger Desteði Ýçin Eklendi ---
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        // -----------------------------------

        builder.Services.AddCommonServiceExt(typeof(BasketAssembly));

        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis");
        });

        builder.Services.AddVersioningExt();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();

            // --- Swagger UI Ýçin Eklendi ---
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                // .NET 9 OpenApi varsayýlan olarak v1.json üretir
                options.SwaggerEndpoint("/openapi/v1.json", "Basket API v1");
            });
            // ------------------------------
        }

        app.AddBasketGroupEndpointExt(app.AddVersionSetExt());

        app.Run();
    }
}