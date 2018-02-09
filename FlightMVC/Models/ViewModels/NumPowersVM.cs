using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightMVC.Models.ViewModels
{
    public class NumPowersVM
    {
        public int Num { get; set; }
        public int Square { get { return Num * Num; } }
        public int Cube { get { return Num * Num*Num; } }
        public double PowerOfNum { get { return Math.Pow(Num , Num); } }
    }
}
