using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceOrder.Domain.Entities
{ //anemic model => rich domain modele çevrilme amaçlanır(yardımcı method eklenmesi) anomıc model sadece proplar varsa 
    public class OrderItem : BaseEntity<int>
    {//OrderItem keni çapında olduğunden int olabilir ama ORDERİN KENİSİ GUİD

        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;//null olamaz dolmak zorunda 
        public decimal UnitPrice { get; set; }
        public Guid  OrderId { get; set; }
        public Order Order { get; set; } = null!;//navigation prop

        #region
        //rich domain : bunlar behavior methotdur
        //behavior methods davranışı etkıleyen methodlardır 
        public void SetItem(Guid productId, string productName, decimal unitPrice)
        {
            if (string.IsNullOrEmpty(ProductName))
            {
                throw new ArgumentNullException(nameof(productName),"ProductName can not is empty");
            }
            if (UnitPrice<=0)
            {
                throw new ArgumentNullException(nameof(unitPrice),"UnitPrice can not be less than equal the zero.");
            }

            this.ProductId=productId;
            this.ProductName=productName;
            this.UnitPrice=unitPrice;
        }

        //fiyatı güncelleme
        public void UpdataPrice(decimal newPrice)
        {
            if (newPrice <= 0) { throw new ArgumentNullException("UnitPrice cannot be less than or equal zero");}
            this.UnitPrice = newPrice;
        }

        //indirim uygulama 
        public void ApplyDiscount(float discountPercentage)
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
