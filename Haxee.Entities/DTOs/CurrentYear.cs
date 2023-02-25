using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxee.Entities.DTOs
{
    public class CurrentYear
    {
        public required string BrokerIP { get; set; }
        public int BrokerPort { get; set; }
        public required string ClientName { get; set; }
        public required string GlobalTopic { get; set; }
        public int Year { get; set; }
        public bool SetupDone { get; set; }

        public void Clear()
        {
            BrokerIP = String.Empty;
            BrokerPort = 0;
            ClientName = String.Empty;
            GlobalTopic = String.Empty;
            Year = 0;
            SetupDone = false;
        }
    }
}
