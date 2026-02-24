using NewMicroservices.Shared;

namespace Microservices.Catalog.Api.Features.Categories.Create
{
    public record CreateCategoryCommand(string Name) :IRequestByServiceResult<CreateCategoryResponse>;//primary constructor olarak inşa edildi
                                                                                                               //string Name : set kısmı init olan ve constuructor ile istenen alan oldu


    //üstteki ile arasında bir fark yok 
    //NESNE ÖRNEĞİ OLDUĞUNDA PROPERTYLERİNDE DEĞİŞİKLİK OLMAZ 
    //public record X
    //{
    //    public string  Name { get; init; }//NESNE ÖRNEĞİ OLDUĞUNDA PROPERTYLERİNDE DEĞİŞİKLİK OLMAZ  BU YUZDEN SET = İNİT OLDU 

    //    public X(string name)
    //    {
    //        Name = name;

    //        var x = new X("kalem");//Namw propertysi değer aldımı daha da değişmez 
    //        //x.Name="defter";//hata verir çünkü init ile inşa edildiği için değiştirilemez
    //    }
    //}

    //CLASSLAR SERVİCELER VB . DURUMLARDA TAŞINACAK DATA İMMUTABLE OLMALI

}
