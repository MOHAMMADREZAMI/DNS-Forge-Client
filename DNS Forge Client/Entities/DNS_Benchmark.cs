using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNS_Forge_Client.Entities
{
    class DNS_Benchmark
    {
        public string Name { get; set; }
        public float Availability {  get; set; }
        public double Latency { get; set; }
    }
}
