namespace Microservices.Catalog.Api.Features.Courses.Create
{
    //request dto : burda ıs değeri alınmayacak 
    public record CreateCourseCommand(string Name,string Description,int Price,string? ImageUrl,Guid CategoryId) :
        IRequestByServiceResult<Guid>;//geriye guid değer doneceğiz create işleminden sora 
    //PictureUrl:resim  ui de kaydedilecek vefile mıcroservisine aydedecek  gelen url bize gonderilecek ve PictureUrl de kaydedeceğiz
}





/*Record, C#'ta "sadece veri" için tasarlanmış, hafif sıklet bir nesne yapısıdır.

En Net Haliyle Record:
Veri Odaklıdır: Amacı iş mantığı (logic) yürütmek değil, veriyi bir yerden bir yere güvenle taşımaktır.

Değişmezdir (Immutable): Bir kez oluşturulur, üzerindeki değerler değiştirilemez (yeni bir kopya oluşturulmadığı sürece).

İçeriğe Bakar: İki farklı Record nesnesinin içindeki veriler aynıysa, C# bunları "aynı şey" olarak kabul eder.

Kod Tasarrufu Sağlar: Normal bir sınıfta (class) onlarca satırda yapacağın işi (Constructor, Equals, ToString vb.) tek satırda halleder.

Kısacası: Eğer elinde sadece okunacak ve taşınacak bir veri grubu varsa (API istekleri, veritabanı modelleri gibi), Class yerine Record kullanmak daha güvenli ve temizdir.*/