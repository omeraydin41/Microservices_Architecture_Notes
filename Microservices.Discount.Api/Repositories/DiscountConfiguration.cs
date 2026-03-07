using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
namespace Microservices.Discount.Api.Repositories
{ 
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Discount> builder)
        {
            builder.ToCollection("courses");//mongoya yansıyacak tablo adını verdik 
            builder.HasKey(x => x.Id);//Id Course üzerinden BaseEntity den geliyor ana key veridi 
            builder.Property(x => x.Id).ValueGeneratedNever();//Id nin veri tabanı tarafından üretilmemesi için
            builder.Property(x => x.Code).HasElementName("name").HasMaxLength(100);
            //guid max 100 karakter// HasElementName ile db ye küçük harfle tutulsun 

            builder.Property(x => x.Description).HasElementName("description").HasMaxLength(1000);
            builder.Property(x => x.Created).HasElementName("created");
            builder.Property(x => x.UserId).HasElementName("userId");
            builder.Property(x => x.CategoryId).HasElementName("categoryId");
            builder.Property(x => x.ImageUrl).HasElementName("ImageUrl").HasMaxLength(200);
            builder.Ignore(x => x.Category);//VERİTABANINA SUTUN OLUŞTURMASIN
        }
    }
}
