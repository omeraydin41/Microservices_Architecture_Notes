using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceOrder.Domain.Entities
{
    public class Address : BaseEntity<int>//BaseEntity generic tiptedir ve içindeki TEntityId' nin tipi int olarak belirlendi
    {
        public string Province { get; set; } = null!;
        public string District { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string Line { get; set; } = null!;

    }
}
