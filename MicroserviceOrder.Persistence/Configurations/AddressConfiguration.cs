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
    public class AddressConfiguration : IEntityTypeConfiguration<Address>//order.domain altında bulunan Adress clasını conf etmek için IEntityTypeConfiguration interfacesını kullanıyoruz.
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Province).HasMaxLength(50).IsRequired();
            builder.Property(x => x.District).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Line).HasMaxLength(200).IsRequired();
            builder.Property(x => x.ZipCode).HasMaxLength(20).IsRequired();
        }
    }
}
