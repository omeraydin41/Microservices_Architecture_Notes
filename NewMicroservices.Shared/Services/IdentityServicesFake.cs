using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMicroservices.Shared.Services
{
    public class IdentityServicesFake : IIdentityServices
    {
        public Guid GetUserId => Guid.Parse("0ea2bc7b-d21e-4526-83a1-82e0a2aa497d");

        public string GetUserName =>"Ömer41";
    }
}
