﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FlightMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FlightMVC.Controllers
{
    [Authorize(Roles="AdminOnly")]
    public class PassengersController : Controller
    {

        static List<PassengerDetails> _passengers = new List<PassengerDetails>();
        static PassengersController()
        {
            for (int i = 0; i < 10; i++)
            {
                _passengers.Add(new PassengerDetails
                {
                    Name = $"Fred{i}",
                    Weight = 10 + i,
                    Prefs = new Preferences { Food = "No restrictions", ExtraLegRoom =(i %2==0) ? true: false }
                });
            }
        }
        // GET: Passengers
        public ActionResult Index()
        {
            return View(_passengers);
        }

        // GET: Passengers/Details/5
        public ActionResult Details(string id)
        {
            return FindAndDisplay(id);
        }

        private ActionResult FindAndDisplay(string id)
        {
            var passenger = _passengers.FirstOrDefault(x => x.Name == id);
            if (passenger == null) return NotFound("passenger not found");
            return View(passenger);
        }

        // GET: Passengers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Passengers/Create
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

        // GET: Passengers/Edit/5
        public ActionResult Edit(string id)
        {

            return FindAndDisplay (id);
        }

        // POST: Passengers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, PassengerDetails collection)
        {                               // defaults to IFormCollection
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here
                    var pd = _passengers.FirstOrDefault(p => p.Name == id);
                    if (pd == null) return NotFound();
                    //PutInSession(pd);
                    PassengerDetails pdOld = pd.Clone() as PassengerDetails;
                    pd.Weight = collection.Weight;

                    PutInSession(pdOld, pd);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return FindAndDisplay(id);
                }
            }
            else {
                return View(collection); }
            }

        public IActionResult Changes()
        {
            var pdTuple = GetFromSession();
            if (pdTuple == null) return NotFound();
            return View(pdTuple);
        }

        private void PutInSession(PassengerDetails pdOld, PassengerDetails pdNew)
        {
            JsonSerializer js = new JsonSerializer();
            StringWriter sw = new StringWriter();
            js.Serialize(sw,new Tuple<PassengerDetails,PassengerDetails>( pdOld,pdNew));
            var jsonOutput = sw.ToString();
            HttpContext.Session.SetString("Change", jsonOutput);

//            HttpContext.Session.SetString("Change",Newtonsoft.Json.JsonConvert.SerializeObject(pd));
        }


        private Tuple<PassengerDetails,PassengerDetails> GetFromSession()
        {
            var jsonInput = HttpContext.Session.GetString("Change");
            //return Newtonsoft.Json.JsonConvert.DeserializeObject<PassengerDetails>(jsonInput);

            JsonSerializer js = new JsonSerializer();
            Tuple<PassengerDetails,PassengerDetails> pd = null ;
            if (!string.IsNullOrEmpty(jsonInput))
            {
                StringReader sw = new StringReader(jsonInput);
                pd = js.Deserialize(sw, typeof(Tuple<PassengerDetails, PassengerDetails>)) as Tuple<PassengerDetails, PassengerDetails>;
            }
            return pd;

        }


        // GET: Passengers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Passengers/Delete/5
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