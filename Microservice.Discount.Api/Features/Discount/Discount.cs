using UdemyNewMicroservice.Discount.Api.Repositories;

namespace Microservice.Discount.Api.Features.Discount
{
    public class Discount:BaseEntity
    {
        public Guid UserId { get; set; }
        public float Rate { get; set; }
        public string Code { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Expired { get; set; }

    }
}
