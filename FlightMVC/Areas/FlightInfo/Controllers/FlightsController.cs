using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightMVC.Areas.FlightInfo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightMVC.Areas.FlightInfo.Controllers
{
    [Area("FlightInfo")]
    public class FlightsController : Controller
    {
        static List<Flight> _flights = new List<Flight>();
        static FlightsController()
        {
            for (int i = 0; i < 2; i++)
            {
                _flights.Add(new Flight { FlightNo = "FLT" + i, Origin = (i % 2 == 0) ? "Bristol" : "Glasgow", Destination = (i % 2 == 1) ? "Bristol" : "Glasgow" });
            }
            
        }


        // GET: Flight
        public ActionResult Index()
        {
            return View(_flights);
        }

        // GET: Flight/Details/5
        public ActionResult Details(string id)
        {
            var flight = _flights.FirstOrDefault(x => x.FlightNo == id);
            if (flight == null) return NotFound();
            return View(flight);
        }

        // GET: Flight/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Flight/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Flight/Edit/5
        public ActionResult Edit(string id)
        {
            return View();
        }

        // POST: Flight/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Flight/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Flight/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}