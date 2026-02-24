


namespace Microservices.Catalog.Api.Features.Categories.Create
{
    public class CreateCategoryCommandHandler(AppDbContext context)//mediatR test surecını kolaylaştırır . test için appdbcontext e ihtiyacımız var veri tabanı bağlantısı için
        : IRequestHandler<CreateCategoryCommand,ServiceResult<CreateCategoryResponse>>
    {
        public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            //category adı daha önce kullanılmış mı kontrol et :  mantıken etme yolu karşılaştıma : VAR OLAN LİSTE İLE İSTEĞİ KARŞILAŞTIMA
            //BİZ DE İSTEK İŞLEMİNİ İŞELYEN CLASSINDAYIZ :  İSTEK CLASSI : CreateCategoryCommand İLE CATEGORY KARŞILAŞTITILACAK
            var existCategory = await context.Categories.AnyAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);
            //1.Name =Category(kategory listesi classı ) clasından gelir 2.Name : CreateCategoryCommand(ategorı oluşturma ) clsından gelr// any varmı 


            if (existCategory)//check etme // BÖYLE BİR KATEGORİ ERROR MESAJI DOLDURULDU SHARED LİBDE TUM UYGULAMADA BU MESAJLAR KULLANILABILIR 
            {//title detail                                //ERROR MESAJLARINDAN STRING STRING BADREQUEST OLAN ERROR MESAJINI DOLDURDUK /Shared CLASS LIB  ServiceResult CLACC İÇİ
                ServiceResult<CreateCategoryResponse>.Error("category name already exist",$"the category name '{request.Name}' already exist",HttpStatusCode.BadRequest);
            }

            //EĞER KATEGORİ YOKSA OLUŞTURULACAK //KATEGORİ OLUŞTUMAK İÇİN CATEGORY DEN NESNE ALINMALI

            var category = new Category
            {
                Name = request.Name,//(Category clasının propertysi)Name = request.Name(üst kısımda sorgudan gelen isim request.Name )

                Id = NewId.NextSequentialGuid() //Id alanı Category classının kalıtım aldığı Base entitiydeki  public Guid Id { get; set; } alanı 
                                                //NewId.NextSequentialGuid indexlenmesi daha kolay ve sıralı guid üretr  Guid.NewGuid() göre
            };

            

            //KATEGORİ OLUŞTU ŞİMDİ EKLEME İŞELMİNDE 
            await context.AddAsync(category,cancellationToken);//cancellationToken : asenkron operasyonları iptal etmek için kullanılır .net kendi fırlatır 
            //cancellationToken mesela web de bir istek yarıda kesilince EXCEPTİON fıralarak durur.ASENKRONLAR EXCEPTİON FIRLATMADAN DURMAZ 


            //VERİ TABANINA KAYDETME İŞLEMİNDE 
            await context.SaveChangesAsync(cancellationToken);


            //SON İŞLEM BAŞARILI OLAN İŞLEMİ MESAJ OLARAK DONMEDE 
            return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id),"empty");


        }
    }
}
