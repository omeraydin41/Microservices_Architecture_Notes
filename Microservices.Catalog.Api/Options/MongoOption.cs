using System.ComponentModel.DataAnnotations;

namespace Microservices.Catalog.Api.Options
{
    public class MongoOption
    {
        [Required]
        public string DatabaseName { get; set; } = default!;// COMPİLERE(derleyıci) SADECE UYARI VERİRİZ ekiptekiler burayı boş bırakmasın diye   
        [Required]
        public string ConnectionString { get; set; } = default!;

    }
}
