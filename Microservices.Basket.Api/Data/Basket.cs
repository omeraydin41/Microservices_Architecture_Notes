namespace Microservices.Basket.Api.Data
{
    public class Basket//ANAMİC MODEL => RİCH DOMAİN MODEL(behavior + data)
    {
        public Guid UserId { get; set; }
        public List<BasketItem> Items { get; set; } = new();
        public float? DiscountRate { get; set; }//indirim oranı// olmak zorunda değil
        public string? Coupon { get; set; }//null olabılır 


        //indirim uygulandımı EVET/HAYIR = BOOL //DiscountRate(indirim oranı)>0 ve kupon nulll olmayacak || iki şart varsa indirim uygulanmıştır
        public bool IsApplyDiscount => DiscountRate is > 0 && !string.IsNullOrEmpty(Coupon);//

        //total price alındı
        public decimal TotalPrice => Items.Sum(x => x.Price);//items listesi BasketItem Tutar ve BasketItem içinde Priceyi aldık

        //indirim uygulanmış totalprice
        public decimal? TotalPriceWithAppliedDiscount
        {
            //indirim uygulayabilmek için şartlar yazılacak //get var sadece
            get
            {
                return !IsApplyDiscount ? null : Items.Sum(x => x.PriceByApplyDiscountRate);
            }
        }
        public Basket(Guid userId,List<BasketItem>items)
        {
            UserId = userId;
            Items = items;

        }
        public Basket()
        {
            
        }
        //ilgili baskete indirim uygula //indirim için yazılan ıkı değişken maplandi
        public void ApplyNewDiscount(string coupon, float discountRate)
        {
            Coupon = coupon;
            DiscountRate = discountRate;

            //indirim uygulama 
            foreach (var basket in Items)
            {
                //indrim uygulanmış fiyat = mevcut olan fiyatı ile 
                basket.PriceByApplyDiscountRate = basket.Price * (decimal)(1 - DiscountRate);
            }
        }
        public void ApplyAviableDiscount()//mevcut ındırım
        {
            foreach (var basket in Items)
            {
                basket.PriceByApplyDiscountRate = basket.Price * (decimal)(1 - DiscountRate);
            }
        }

        public void ClearDiscount()
        {
            DiscountRate = null;
            Coupon = null;
            foreach (var basket in Items)
            {
                basket.PriceByApplyDiscountRate = null!;
            }
        }

    }
}
