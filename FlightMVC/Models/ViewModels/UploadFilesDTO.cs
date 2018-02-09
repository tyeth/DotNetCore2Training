using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightMVC.Models.ViewModels
{
    public class UploadFilesDTO
    {
        public IFormFile File { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
