using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace DNS_Forge_Client.Entities
{
    [MessagePackObject]
    class DNS
    {
        [Key(0)]
        public DNS_Template[] IR { get; set; }
        [Key(1)]
        public DNS_Template[] Global { get; set; }
    }
}
