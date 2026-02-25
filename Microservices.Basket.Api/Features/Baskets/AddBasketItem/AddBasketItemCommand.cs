using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Microservices.Basket.Api.Features.Baskets.AddBasketItem
{
    public record AddBasketItemCommand(Guid CourseId ,string CourseName ,decimal CoursePrice ,string ImageUrl);
    //amaç send-kron şsteklerın sayısını azaltamak olduğundan dolayı her serviste aynı alanlar tutulabılır.

    //AddBasketItemCommand bir Entity(Varlık) değil, bir DTO(Data Transfer Object) türüdür.
    //Command: Kullanıcının "Sepete şu ürünü eklemek istiyorum" talebini taşıyan bir zarftır. Geçicidir.


}
