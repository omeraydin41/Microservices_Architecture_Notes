namespace Microservices.Basket.Api.Data
{
    public class BasketItem
    {
        public Guid Id {  get; set; }
        public string Name { get; set; } = default!;
        public string? imageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceByApplyDiscountRate { get; set; }//indirim uygulandıktan sonra fiyat null olabilir çünkü indirim uygulanmayabilir
    }
}
