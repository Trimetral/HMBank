using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainLibrary.Clients
{
    class NullClient : Client
    {
        public NullClient() : base(0, 0, "Not initialised", "Not initialised", "-1") { }
    }
}
