using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FlightMVC.Filters;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.IO;

namespace FlightMVC.Controllers
{
    [ServiceFilter(typeof(MyExceptionFilterAttribute))]
    [Route("theRoot")]
    public class DemosController : Controller
    {
        private ILogger _logger;
        public DemosController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("**** <><><> Demo Controller Logger");
        }


        [HttpGet("UploadDemo")]
        public IActionResult UploadDemo()
        {
            return View();
        }


        [HttpPost("UploadDemo")]
        public async Task<IActionResult> UploadDemo(UploadFilesDTO dto)
        {
            var list = new List<string>();

            if (dto.File != null)
            {
                string filePath = Path.GetTempFileName();  // only 65000 temp files allowed ( UBound(short) )
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    await dto.File.CopyToAsync(fs);
                }
                list.Add(dto.File.FileName + " -> " + filePath);
            }

            if (dto.Files != null)
            {
                foreach (var item in dto.Files)
                {
                    if (item == null) continue;
                    string filePath = Path.GetTempFileName();  // only 65000 temp files allowed ( UBound(short) )
                    using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                    {
                        await item.CopyToAsync(fs);
                    }
                    list.Add(item.FileName + " -> " + filePath);
                }
            }

            if (list.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "file needs selecting first mate.");
            }

            ViewBag.Results = list;

            return View();
        }

        [TypeFilter(typeof(MyActionFilterAttribute))]
        [HttpGet("Action")]
        public IActionResult ActionFilterDemo()
        {
            _logger.LogInformation("++++++++++++++++++++++++++++++++ In ActionDemo +++++++++++++++++++++++++++");
            return View();
        }


        [HttpGet("Powers")]
        public IActionResult Powers()
        {
            List<NumPowersVM> list = new List<NumPowersVM>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new NumPowersVM() { Num = i });
            }
            return View(list);
        }

        [HttpGet("Problem")]
        public IActionResult Problem()
        {
            throw new ApplicationException();
        }


        [Route("selectdemo")]
        public IActionResult SelectDemo()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "one", Value = "1", Selected = false });
            list.Add(new SelectListItem() { Text = "two", Value = "2", Selected = false });
            list.Add(new SelectListItem() { Text = "three", Value = "3", Selected = true });
            ViewData["Items"] = list;
            return View();
        }

        [Route("~/noroot/index")]
        public IActionResult NewIndex()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("~/tyeth/square/{val:int}/{str?}")]
        public IActionResult Square(int val, string str = "")
        {
            return new ContentResult() { Content = (val * val) + str.ToString() };
        }

        [HttpGet("square/{val:int}")]
        public IActionResult Square(int val)
        {
            return new ContentResult() { Content = (val * val).ToString() };
        }


        [HttpGet("square/{val:alpha}")]
        public IActionResult Square(string val)

        {
            int result = 0;
            switch (val)
            {
                case "two":
                    result = 4; break;

                case "three":
                    result = 9; break;

                case "four":
                    result = 16; break;

                default:
                    break;
            }
            return new ContentResult() { Content = (result).ToString() };
        }
    }
}