using MicroserviceOrder.Persistence;
using Microsoft.EntityFrameworkCore;
using static MicroserviceOrder.Persistence.AppDbContext;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));//appsettings.Development.json içinden gelen con string

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();

