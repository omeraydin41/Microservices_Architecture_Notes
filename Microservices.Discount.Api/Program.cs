using Microservices.Discount.Api;

var builder = WebApplication.CreateBuilder(args);

// 1. AddOpenApi() satýrýný sildik çünkü Swashbuckle ile çakýþýyor.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCommonServiceExt(typeof(DiscountAssembly));
//builder.Services.AddVersioningExt();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // 2. MapOpenApi() satýrýný sildik, çakýþmanýn ana sebebi buydu.
}

app.Run();