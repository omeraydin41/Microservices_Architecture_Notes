using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceOrder.Domain.Entities
{
    public class Order:BaseEntity<Guid>//BaseEntity içindeki Id değeri burda Guid olarak tutulacak
    {
        
        public string Code { get; set; } = null!;
        public DateTime Created { get; set; }
        public Guid BuyerId { get; set; }
        public OrderStatus Status { get; set; }
        public  int AddressId { get; set; }//1 order birden fazla addres alabılır 
        public decimal TotalPrice {  get; set; }
        public float? DiscountRate {  get; set; }
        public Guid PayementId {  get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
        public Address Address { get; set; } = null!;//null olamaz

        public static string GenerateCode()
        {
            var random = new Random();
            var orderCode = new StringBuilder(10);//StringBuilder değiştirilebilir (mutable) bir nesnedir.
                                                           //Metni mevcut hafıza bloğu üzerinde günceller, yeni kopyalar oluşturup sistemi yormaz. 
            for (var i = 0; i < 10; i++)
            {
                orderCode.Append(random.Next(0, 10));
            }
            return orderCode.ToString();
        }

        public static Order CreateUnPaidOrder(Guid buyerId,float discountRate,Address adressId)
        {
            return new Order
            {
                Id=NewId.NextGuid(),
                Code = GenerateCode(),
                BuyerId = buyerId,
                Created= DateTime.Now,
                Status = OrderStatus.WaitingForPayment,
                AddressId = adressId.Id,
                TotalPrice = 0,
                OrderItems = new List<OrderItem>(),
                DiscountRate = discountRate
            };         
        }

        public void AddOrderItem(Guid productId,string productName,decimal unitPrice)
        {
            var orderItem = new OrderItem();
            orderItem.SetItem(
                productId,
                productName,
                unitPrice);
            OrderItems.Add(orderItem);

            CalculateTotalPrice();
        }

        //total price yardımcı method 
        private void CalculateTotalPrice()
        {
            TotalPrice=OrderItems.Sum(x=>x.UnitPrice);
            if (DiscountRate.HasValue)
            {
                TotalPrice -= TotalPrice * (decimal)DiscountRate.Value / 100;
            }
        } 

        public void SetPaidStatus(Guid PaymentId)
        {
          Status= OrderStatus.Paid;
            this.PayementId= PaymentId;
        }


    }
}
