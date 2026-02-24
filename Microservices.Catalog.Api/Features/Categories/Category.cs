using Microservices.Catalog.Api.Features.Courses;
using Microservices.Catalog.Api.Repostories;

namespace Microservices.Catalog.Api.Features.Categories
{
    public class Category: BaseEntity//Repostories klasorundaki BaseEntity den kalıtım alıyor : içi   [BsonElement("_id")]public Guid Id { get; set; }
    {
        public string Name { get; set; } = default!;//mutlaka doldurulması gereken alan deafoult olamaz 

        public List<Course>? Courses { get; set; }// her kursun sadece bir kategorisi olabilir ama
                                                  // Bir kategorinin birden fazla kursu olabilir
                                                  //Bir kategorinin kursları OLMAYADABİLİR

        //? verilmesi compilerin değişken hakkındaki uyarılarını kapatır null olabilir demek 
    }

    //Bir kategorinin altında birden fazla kurs bulunabileceği için,
    //bu çoklu yapıyı kod tarafında bir arada tutacak bir "konteynere" (liste yapısına) ihtiyaç duyarız.
}
