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
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>//OrderItem classınının  ayaralarını yapacağız **IEntityTypeConfiguration**, veritabanı yapılandırma kurallarını (tablo isimleri,
                                                                             //kolon özellikleri, ilişkiler) Entity sınıflarını kirletmeden ayrı sınıflarda tanımlayarak kodun temiz ve
                                                                             //modüler kalmasını sağlayan bir yapıdır.
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.ProductName).HasMaxLength(200).IsRequired();
            builder.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)").IsRequired();

        }
    }
}
