using System.Text.Json.Serialization;
namespace Microservices.Basket.Api.Dto
{
    public record BasketDto
    //sepete karşılık gelen bir dto oluşturduk. UserId yi tutacak
    //BasketItems ise sepetteki ürünleri tutacak bir liste olacak. BasketItemDto türünde olacak
    {

        [JsonIgnore] public bool IsApplyDiscount => DiscountRate is > 0 && !string.IsNullOrEmpty(Coupon);

        public List<BasketItemDto> Items { get; set; } = new();
        public float? DiscountRate {  get; set; }
        public string?  Coupon { get; set; }


        public decimal TotalPrice=>Items.Sum(x=>x.Price);

        public decimal? TotalPriceWithAppliedDiscount
        {
            //indirim uygulayabilmek için şartlar yazılacak //get var sadece
            get
            {
                return !IsApplyDiscount ? null : Items.Sum(x => x.PriceByApplyDiscountRate);
            }
        }

        public BasketDto(List<BasketItemDto> itmes) 
        {
           
            Items = itmes;
        }
        public BasketDto()
        {
            
        }
    }
}
