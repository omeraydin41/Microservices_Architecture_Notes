using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceOrder.Domain.Entities
{ //anemic model => rich domain modele çevrilme amaçlanır(yardımcı method eklenmesi)
    public class OrderItem : BaseEntity<int>
    {//OrderItem keni çapında olduğunden int olabilir ama ORDERİN KENİSİ GUİD

        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = default!;//null olamaz dolmak zorunda 
        public decimal UnitPrice { get; set; }



        #region
        //rich domain : bunlar behavior methotdur

        public void SetItem(Guid ProductId, string ProductName, decimal UnitPrice)
        {
            if (string.IsNullOrEmpty(ProductName))
            {
                throw new ArgumentNullException("ProductName can not is empty");
            }
            if (UnitPrice<=0)
            {
                throw new ArgumentNullException("UnitPrice can not be less than equal the zero.");
            }

            this.ProductId=ProductId;
            this.ProductName=ProductName;
            this.UnitPrice=UnitPrice;
        }

        //fiyatı güncelleme
        public void UpdataPrice(decimal newPrice)
        {
            if (newPrice <= 0) { throw new ArgumentNullException("UnitPrice cannot be less than or equal zero");}
            this.UnitPrice = newPrice;
        }

        //indirim uygulama 
        public void ApplyDiscount(double discountPercentage)
        {
            if (discountPercentage<0||discountPercentage>100)
            {
                throw new ArgumentNullException("Discount Percentage must be between 0 and 100.");
            }
            this.UnitPrice -= (this.UnitPrice * (decimal)discountPercentage / 100);
        }

        public bool IsSameItem(OrderItem otherIthem)
        {
            return this.ProductId == otherIthem.ProductId;
        }
        #endregion

    }
}
