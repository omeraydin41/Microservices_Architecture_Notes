//using Microservices.Catalog.Api.Features.Categories;

using Microservices.Catalog.Api.Features.Categories;
using Microservices.Catalog.Api.Features.Courses;

namespace Microservices.Catalog.Api.Repostories
{
    public static class SeedData
    {
        public static async Task AddSeedDataExt(this WebApplication app)//WebApplication program.cs deki r app = builder.Build(); kısmı
        {

            using var scope = app.Services.CreateScope();//scope üzerinden DI da bulunan nesnlere erişim sağlayacağız.using bittiği anda otomatık dispoze olacak
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();//AppDbContext nesnesine erişim sağladık ve dbContext üzerinden veritabanına erişim sağlayacağız

            dbContext.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;//transaction desteğini kladırdık 

            // Kategoriler için örnek veri Yok ise
            if (!dbContext.Categories.Any())//Any : boş ise 
            {
                var categories = new List<Category>
                {
                    new(){Id=NewId.NextSequentialGuid(),Name="Devolopment"},
                    new(){Id=NewId.NextSequentialGuid(),Name="Business"},
                    new(){Id=NewId.NextSequentialGuid(),Name="Ir & Software"},
                    new(){Id=NewId.NextSequentialGuid(),Name="OfficeProductivity"},
                    new Category(){Id=NewId.NextSequentialGuid(),Name="PersonalDevelopment" }
                };

                //alınan Bu kategori adları kategoriye eklensin
                dbContext.Categories.AddRange(categories);//addrenge birden fazla veri eklemek için kullanılır add tek tek ekler addrange ise toplu ekleme yapar
                await dbContext.SaveChangesAsync();//değişiklikleri kaydetmek için kullanılır ve asenkron olarak çalışır
            }

            //Yukarda Category verisi yoksa ekleme yapılacaktır 
            //Simdi ise Db de Course verisi yoksa eklem yapacağız 

            if (!dbContext.Courses.Any())//Any : boş ise 
            {
                //kurs eklemek için önce kategori Id ihtiyacımız var çünkü kursların kategorisi olacak

                var category= await dbContext.Categories.FirstAsync();//ilk gelen kategoriyi aldık 

                var randomUserId = NewId.NextGuid();

                List<Course> courses =
                [
                new()
                {
                    Id = NewId.NextSequentialGuid(),//NextSequential(Sıralı)Guid : Guid türünde sıralı benzersiz bir kimlik oluşturur.
                                                    //Bu, veritabanlarında performansı artırmak için kullanılabilir
                                                    //çünkü sıralı GUID'ler indeksleme ve sorgulama işlemlerini optimize eder.Snow flake kullanır
                                                    //indexlenemiyen sütunlarda next guid ,indexlenebilen sütunlarda next sequential guid kullanılır
                    Name = "C#",
                    Description = "C# Course",
                    Price = 100,
                    UserId = randomUserId,//random olarak userıd verdik 
                    Created = DateTime.UtcNow,//global uygulmalarda tarih ve saat bilgisi için UTC kullanılır .zone(alan) bilgisi içemez 
                    Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" },
                    CategoryId = category.Id
                },
                new()
                {
                    Id = NewId.NextSequentialGuid(),
                    Name = "Java",
                    Description = "Java Course",
                    Price = 200,
                    UserId = randomUserId,
                    Created = DateTime.UtcNow,
                    Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" },
                    CategoryId = category.Id
                },

                new()
                {
                    Id = NewId.NextSequentialGuid(),
                    Name = "Python",
                    Description = "Python Course",
                    Price = 300,
                    UserId = randomUserId,
                    Created = DateTime.UtcNow,
                    Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" },
                    CategoryId = category.Id
                }
                ];


                dbContext.Courses.AddRange(courses);//addrenge birden fazla veri eklemek için kullanılır
                                                            //add tek tek ekler addrange ise toplu ekleme yapar

                await dbContext.SaveChangesAsync();//değişiklikleri kaydetmek için kullanılır ve asenkron olarak çalışır

            }

        }
    }
}
