namespace Microservices.Basket.Api.Dto
{
    public record BasketItemDto(//sepet içerisindeki itemleri temsil eden bir dto oluşturduk
        Guid Id,
        string Name,
        string imageUrl,
        decimal Price,
        decimal? PriceByApplyDiscountRate//indirim uygulandıktan sonra fiyat null olabilir çünkü indirim uygulanmayabilir
        );
   
}
