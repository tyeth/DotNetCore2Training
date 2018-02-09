using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightMVC.Models
{
    public class Preferences
    {
        public string Food { get; set; }
        [Display(Name="Extra Leg Room")]
        public bool ExtraLegRoom { get; set; }
    }
    public partial class PassengerDetails:ICloneable
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public Preferences Prefs { get; set; }

        public object Clone()
        {
            return base.MemberwiseClone();
        }
    }
}
