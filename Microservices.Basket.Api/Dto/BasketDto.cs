namespace Microservices.Basket.Api.Dto
{
    public record BasketDto(Guid UserId,List<BasketItemDto> BasketItems)
    //sepete karşılık gelen bir dto oluşturduk. UserId yi tutacak
    //BasketItems ise sepetteki ürünleri tutacak bir liste olacak. BasketItemDto türünde olacak
    {
    }
}
