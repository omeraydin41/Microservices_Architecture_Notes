using Microservices.Catalog.Api.Features.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Microservices.Catalog.Api.Repostories
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //Category de sadece Id değeri var 
            builder.ToCollection("categories");//mongoya yansıyacak tablo adını verdik
            builder.HasKey(x => x.Id);//Category tablosundaki her bir satırı birbirinden ayıran tek şey Id sütunudur
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Ignore(x => x.Courses);//VERİTABANINA SUTUN OLUŞTURMASIN






            //course içindeki Feature propun class ayarları yapıldı cunku bizim iş akışımızda sadec 1 yerde var 
            //her şey field olarak mongo db de saklanacak ama bu hariç 
            //public Category Category { get; set; } = default!; //BU ALANI COURSE İÇİN İGNORE ET 
            //category ve course ayrı ayrı collectionlarda saklanacak 


            //IGNORE İŞLEMİ : NAVIGATİON MAPLAR İŞ AKIŞIDA IKI YERDE OLACAĞINDAN İGNORE EDİYORUZ
            //COURSE İÇİ : //public Category Category { get; set; } = default!;
            //CATEGORY İÇİ : //public List<Course>? Courses { get; set;
        }
    }
}
