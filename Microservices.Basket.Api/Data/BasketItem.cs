namespace Microservices.Basket.Api.Data
{
    public class BasketItem
    {
        public Guid Id {  get; set; }
        public string Name { get; set; } = default!;
        public string? imageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceByApplyDiscountRate { get; set; }//indirim uygulandıktan sonra fiyat null olabilir çünkü indirim uygulanmayabilir

        public BasketItem(Guid id, string name, string? imageUrl, decimal price, decimal? priceByApplyDiscountRate)
        {
            Id = id;
            Name = name;
            this.imageUrl = imageUrl;
            Price = price;
            PriceByApplyDiscountRate = priceByApplyDiscountRate;
        }
    }
}
