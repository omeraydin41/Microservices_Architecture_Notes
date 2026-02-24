namespace Microservices.Catalog.Api.Features.Categories.Dtos
{
    public record CategoryDto(Guid Id,string Name);//immutable nesnelerle çalışacağız 
    /*immutable : Bir nesne oluşturulduktan sonra, o nesnenin içindeki verilerin hiçbir şekilde değiştirilememesi durumuna  denir.*/

    /*Normal bir sınıfta (class), bir özelliği (property) istediğin zaman güncelleyebilirsin. 
      Ancak immutable bir yapıda, veri sadece nesne ilk yaratıldığı an (constructor aşamasında) belirlenir. 
      Eğer veriyi değiştirmek istersen, mevcut olanı değiştirmek yerine yeni bir kopya oluşturman gerekir.*/


    /*C# 9.0 ile gelen record türü, aslında temelinde bir class'tır ancak özellikle veri taşımak (DTO) için optimize edilmiştir. 
      Kodda kullandığı yazım şekline positional record denir*/
}
