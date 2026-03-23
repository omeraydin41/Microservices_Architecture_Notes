#region
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MongoDB.Driver;

#endregion

namespace NewMicroservice.Discount.Api.Repositories;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{

    // Satırı şu şekilde güncelle:
    public DbSet<Microservice.Discount.Api.Features.Discount.Discount> Discounts { get; set; }

    public static AppDbContext Create(IMongoDatabase database)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<AppDbContext>().UseMongoDB(database.Client,
                database.DatabaseNamespace.DatabaseName);


        return new AppDbContext(optionsBuilder.Options);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}