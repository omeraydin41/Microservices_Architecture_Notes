using System.Text.Json.Serialization;
namespace Microservices.Basket.Api.Dto
{
    public record BasketDto
    //sepete karşılık gelen bir dto oluşturduk. UserId yi tutacak
    //BasketItems ise sepetteki ürünleri tutacak bir liste olacak. BasketItemDto türünde olacak
    {

        [JsonIgnore]public Guid UserId { get; init; }//bır kere init yapıldıktan sonra daha değiştirilemesin

        public List<BasketItemDto> Items { get; set; } = new();

        public BasketDto(Guid userId,List<BasketItemDto> itmes) 
        {
            UserId = userId;
            Items = itmes;
        }
        public BasketDto()
        {
            
        }
    }
}
