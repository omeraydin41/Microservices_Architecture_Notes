namespace Microservices.Catalog.Api.Features.Categories.Create
{
    public record CreateCategoryResponse(int Id);//geriye DB de oluşan datanın ıd si dönecek 
    //record dan dolayı immutabe class : int Id  propetıysınde set kısmı init olan ve contructorda bu değeri bekleyen 
    //bir kez alındığında değiştirilemez yapı 

}
