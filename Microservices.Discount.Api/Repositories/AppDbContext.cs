using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection;

namespace Microservices.Discount.Api.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)
    {

        //dbcontext yanı ef core ler veri tabanını soyutlar .mongodan ssms kodların kırlmaması lazım 

      


        public static AppDbContext Create(IMongoDatabase database)//geriye AppDbContext classını dönen method//(hangı db ile çalışacaksa )
        {// bu yardımcı method kullanılarak bu class tan nesen alınacaksa class içersi buraya aktarılmalı 


            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName);
            //DbContextOptionsBuilder : bu alanın kullanılma sebebı class başlangıcında DbContextOptions<AppDbContext>kullanılması 
            //DbContextOptions erişe bılmek için once bunu urettık ve üretırkende hangı db kullanlıcağını belirttık
            //.UseMongoDB mongodb kullanmayı seçtik ve client bilgisini verdik

            return new AppDbContext(optionsBuilder.Options);
        }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {     
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //çalıştığımız assembly dıntercasındekı tum konfşgurasyonları almaya yarar 
            //Assembly yanı projenın ısmını ıster 
            //konfiogurasyonlar  Feature klasorundekı CategoryEntityConfiguration ve CourseEntityConfiguration claslarına dağıttık 
        }


    }
}
