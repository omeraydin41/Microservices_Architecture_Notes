using NewMicroservices.Shared;

namespace Microservices.Basket.Api.Features.Baskets.AddBasketItem
{
    public record AddBasketItemCommand(Guid CourseId ,string CourseName ,decimal CoursePrice ,string? ImageUrl): IRequestByServiceResult;
    //amaç send-kron şsteklerın sayısını azaltamak olduğundan dolayı her serviste aynı alanlar tutulabılır.

    //AddBasketItemCommand bir Entity(Varlık) değil, bir DTO(Data Transfer Object) türüdür.
    //Command: Kullanıcının "Sepete şu ürünü eklemek istiyorum" talebini taşıyan bir zarftır. Geçicidir.


}
