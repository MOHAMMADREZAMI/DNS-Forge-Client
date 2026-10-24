using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace DNS_Forge_Client.Entities
{
    [MessagePackObject]
    class DataLoad
    {
        [Key(0)]
        public string Adapter {  get; set; }
        [Key(1)]
        public string DNS { get; set; }
    }
}
