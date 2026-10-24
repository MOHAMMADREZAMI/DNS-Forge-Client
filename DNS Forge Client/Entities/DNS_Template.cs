using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace DNS_Forge_Client.Entities
{
    [MessagePackObject]
    class DNS_Template
    {
        [Key(0)]
        public string Name { get; set; }
        [Key(1)]
        public string Description { get; set; }
        [Key(2)]
        public string Primary {  get; set; }
        [Key(3)]
        public string Secondary { get; set; }
        [Key(4)]
        public string Logo { get; set; }
    }
}
