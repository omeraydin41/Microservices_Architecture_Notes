using Microservices.Catalog.Api.Features.Categories;
using Microservices.Catalog.Api.Repostories;

namespace Microservices.Catalog.Api.Features.Courses
{
    public class Course : BaseEntity//Repostories klasorundaki BaseEntity den kalıtım alıyor : içi   [BsonElement("_id")]public Guid Id { get; set; }
    {
        public string Name { get; set; } = default!;//kurs adı // bu alanı null bırakma demek HATA VERMEZ KODU AÇIKLAR 
        public string Description { get; set; } = default!;//açıklaması 
        public decimal Price { get; set; }//fiyatı 
        public Guid UserId { get; set; }//Kursu satın alanlar için oluşan alan
        public string? Picture { get; set; }//kursun resmi ? null olabılır 
        //strıng zaten null burdakı amacı artık nullable ozellığı açık gelir . Compiler null olabılecek yerlerde uyarı verir.null olabılır
        //diyerek compiler a uyarı verme deriz . ekip içi çalışmada yardımcı olur. hangı değişkenlerin doldurulması konusunda.



        public DateTime Created { get; set; }//kursun oluşturulma tarihi
        public Guid CategoryId { get; set; }//kursun kategorisi
        public Category Category { get; set; } = default!; //her kursun bir kategorisi olmak zorunda 
        //Course den Category e bağlanmak için her kurs kategoriye bağlı olacak ef core yaklaşımı 

        public Feature Feature { get; set; } = default!;//HER KURSUN BİR ÖZELLİĞİ OLMAK ZORUNDA AMA COMPİLERE SADECE UYARI VERİRİZ . ISTERSK NULL OLABILIR  NİYET BELLİ EDİYORUZ
        //her kursun bir özelliği olmak zorunda // Navigation Property ile kurs özlleikleri FEATURE CLASSINDAN ALINDI 
    }
}
