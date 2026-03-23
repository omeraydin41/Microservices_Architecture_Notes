
using Microservices.Discount.Api.Repositories;
namespace Microservices.Discount.Api.Features.Discounts
{
    public class Discount:BaseEntity
    {
        public Guid UserId { get; set; }
        public float Rate { get; set; }//indirim oranı 
        public string Code { get; set; }//indirim kodu 
        public DateTime Created { get; set; }//indirim oluşturulma tarihi
        public DateTime? Updated { get; set; }//indirimi guncelleme tarıhı 
        public DateTime Expired { get; set; }//indirimin geçersiz olacapı tarih


    }
}
