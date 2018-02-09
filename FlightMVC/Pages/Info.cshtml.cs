using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FlightMVC.Pages
{
    public class InfoModel : PageModel
    {
        private ILogger _log = null;
        public InfoModel(ILoggerFactory logger)
        {
            _log = logger.CreateLogger("InfoModelLogger");
        }

        [BindProperty]
        public string Message { get; set; }
        public void OnGet()
        {
            Message = "Greetings!";

        }


        //public void OnPost()
        //{
        //    _log.LogInformation($"************************OnPost Message={Message} ***************************");
        //}

        public async void OnPostAsync()
        {
            _log.LogInformation($"************************OnPostAsync Message={Message} ***************************");
        }
    }
}