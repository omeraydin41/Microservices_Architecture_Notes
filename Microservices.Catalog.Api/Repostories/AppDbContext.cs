using Microservices.Catalog.Api.Features.Categories;
using Microservices.Catalog.Api.Features.Courses;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection;

namespace Microservices.Catalog.Api.Repostories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)
    {

        //dbcontext yanı ef core ler veri tabanını soyutlar .mongodan ssms kodların kırlmaması lazım 

        public DbSet<Course> Courses { get; set; }//featurede burda var 
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //çalıştığımız assembly dıntercasındekı tum konfşgurasyonları almaya yarar 
            //Assembly yanı projenın ısmını ıster 


        }


    }
}
