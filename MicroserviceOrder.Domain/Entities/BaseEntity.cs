using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceOrder.Domain.Entities
{
    public class BaseEntity<TEntityId>//generic olarak oluşan ANA entity
    {
        public TEntityId Id { get; set; } = default!;
    }
}
