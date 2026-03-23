using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Microservices.Discount.Api.Features.Discounts;
namespace Microservices.Discount.Api.Repositories
{ 

    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {

        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            ArgumentNullException.ThrowIfNull(builder);
            builder.ToCollection("courses");//mongoya yansıyacak tablo adını verdik 
            builder.HasKey(x => x.Id);//Id Course üzerinden BaseEntity den geliyor ana key veridi  ////BASE ENTİTYDE GELİR
            builder.Property(x => x.Id).ValueGeneratedNever();//Id nin veri tabanı tarafından üretilmemesi için//BASE ENTİTYDE GELİR
            builder.Property(x => x.Code).HasElementName("code").HasMaxLength(10);
            //guid max 100 karakter// HasElementName ile db ye küçük harfle tutulsun 

            builder.Property(x => x.Rate).HasElementName("rate");
            builder.Property(x => x.UserId).HasElementName("user_id");

            builder.Property(x => x.Created).HasElementName("created");
            builder.Property(x => x.Updated).HasElementName("updated");
            builder.Property(x => x.Expired).HasElementName("expired");

        }
    }
}
