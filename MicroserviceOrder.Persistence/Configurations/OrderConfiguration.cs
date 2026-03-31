using MicroserviceOrder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceOrder.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();//id biz ureteceğiz never kullanıldı 
            builder.Property(x => x.Code).HasMaxLength(10).IsRequired();
            builder.Property(x => x.BuyerId).IsRequired();
            builder.Property(x => x.Created).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.AddressId).IsRequired();
            builder.Property(x => x.TotalPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.DiscountRate).HasColumnType("float");

            builder.HasMany(x => x.OrderItems).WithOne(x => x.Order).HasForeignKey(x => x.OrderId);//bire çok ilişki
            builder.HasOne(x => x.Address).WithMany().HasForeignKey(x => x.AddressId);//bire çok ilişki
        }
    }
}
