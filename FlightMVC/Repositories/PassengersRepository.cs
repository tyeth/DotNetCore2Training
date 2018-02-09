using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlightMVC.Repositories
{
    public class PassengersRepository : IPassengersRepository
    {
        static List<PassengerDetails> _passengers = new List<PassengerDetails>();

        public PassengersRepository([FromServices]ILoggerFactory factory)
        {
            factory.CreateLogger("log1").LogInformation("INFORMATION IS HERE!<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
        }
        static PassengersRepository()
        {
            for (int i = 0; i < 10; i++)
            {
                _passengers.Add(new PassengerDetails
                {
                    Name = $"Dave{i}",
                    Weight = 10 + i,
                    Prefs = new Preferences { Food = "No restrictions", ExtraLegRoom = (i % 2 == 0) ? true : false }
                });
            }
        }

        public IEnumerable<PassengerDetails> GetPassengers()
        {
            return _passengers;
        }

        public void Add(PassengerDetails value)
        {
            _passengers.Add(value);
        }

        public void Delete(PassengerDetails pd)
        {
            try
            {
                var origP = _passengers.Find(p => p == pd);
                if (origP != null) _passengers.Remove(origP);
            }
            catch (Exception)
            {

                throw;
            }
            
        }


        public void Delete(string id)
        {
            try
            {
                var origP = _passengers.FirstOrDefault(p => p.Name == id);
                if (origP != null) _passengers.Remove(origP);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
