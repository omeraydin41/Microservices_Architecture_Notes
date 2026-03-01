using NewMicroservices.Shared;

namespace Microservices.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public record DeleteBasketItemCommand(Guid Id):IRequestByServiceResult;//IRequestByServiceResult  sahredden gelır 
                                                                                 //silme işlemi ID y egore yapıldığından CourseId adı diğer alanlarla aynı ada sahıp olmalı 

}
