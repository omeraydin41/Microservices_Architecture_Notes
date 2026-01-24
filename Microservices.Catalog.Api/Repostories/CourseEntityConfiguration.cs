using Microservices.Catalog.Api.Features.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Microservices.Catalog.Api.Repostories
{
    public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            //nosqlde Collecction(tablo) Document(satır) Field(sutün)

            builder.ToCollection("courses");//mongoya yansıyacak tablo adını verdik 
            builder.HasKey(x => x.Id);//Id Course üzerinden BaseEntity den geliyor ana key veridi 
            builder.Property(x => x.Id).ValueGeneratedNever();//Id nin veri tabanı tarafından üretilmemesi için
            builder.Property(x => x.Name).HasElementName("name").HasMaxLength(100);
            //guid max 100 karakter// HasElementName ile db ye küçük harfle tutulsun 

            builder.Property(x => x.Description).HasElementName("description").HasMaxLength(1000);
            builder.Property(x => x.Created).HasElementName("created");
            builder.Property(x => x.UserId).HasElementName("userId");
            builder.Property(x => x.CategoryId).HasElementName("categoryId");
            builder.Property(x => x.Picture).HasElementName("picture");
            builder.Ignore(x => x.Category);//VERİTABANINA SUTUN OLUŞTURMASIN

            //feturesın ıd sı olmadığından OWNED TYPE DİR COURSE İÇİNDE GÖMULU OLDUĞUNDAN VE BIR TANE BU YAPIDA OLAN CLASS OLDUPĞUNDAN
            //OwnsOne İLE ulaşıp Feature clasının yansıtma ayararını yaptık 
            builder.OwnsOne(c => c.Feature, feature =>
            {
                feature.HasElementName("feature");//mongoda feature olarak sakla
                feature.Property(f => f.Duration).HasElementName("duration");//feturenın popertysini mongoda duration olarak sakla
                feature.Property(f => f.Rating).HasElementName("rating");
                feature.Property(f => f.EducatorFullName).HasElementName("educatorFullName");
            });
        }
    }
}
