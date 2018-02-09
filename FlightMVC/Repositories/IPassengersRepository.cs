using FlightMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightMVC.Repositories
{
   public interface IPassengersRepository
    {
        IEnumerable<PassengerDetails> GetPassengers();
        void Add(PassengerDetails value);
        void Delete(PassengerDetails value);
        void Delete(string id);
    }
}
