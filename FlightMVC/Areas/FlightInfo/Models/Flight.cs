using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightMVC.Areas.FlightInfo.Models
{
    public class Flight
    {
        public string FlightNo { get; set; }
        public string Origin { get; set; }
        public string  Destination { get; set; }
    }
}
