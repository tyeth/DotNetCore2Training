using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightMVC.Models
{
    [ModelMetadataType(typeof(PassengerDetailsMetadata))]
    public partial class PassengerDetails
    {


        public class PassengerDetailsMetadata
        {
            [Required]
            [Display(Name="Client Name")]
            [StringLength(50, ErrorMessage = "{0} length less than or equal {1}")]
            public string Name { get; set; }

            [Required]
            [Range(1,50, ErrorMessage = "{0} between {1} and {2}, inclusive")]
            public int Weight { get; set; }



        }
    }
}
