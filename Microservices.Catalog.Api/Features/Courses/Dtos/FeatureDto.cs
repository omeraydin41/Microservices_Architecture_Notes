namespace Microservices.Catalog.Api.Features.Courses.Dtos
{
    public class FeatureDto
    {
        public int Duration { get; set; }
        public float Rating { get; set; }
        public string EducatorFullName { get; set; } = default!;//eğitmenın adı null olamaz her kursun eğitmeni olmak zounda 

        //Bu bilgileri Navigation Property ile course de yakalayalım ki tum kurs bilgilri tek bir yerde toplanabilsin
    }
}
