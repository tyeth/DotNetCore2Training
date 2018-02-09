using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightMVC.Models;
using FlightMVC.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightMVC.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/PassengersAPI")]
    public class PassengersAPIController : Controller
    {
        IPassengersRepository _repo = null;

        public PassengersAPIController(IPassengersRepository pRepo)
        {
            _repo = pRepo;
        }

        // GET: api/PassengersAPI
        [HttpGet]
        public IEnumerable<PassengerDetails> Get()
        {
            return _repo.GetPassengers();
        }

        /// <summary>
        /// Get a list of potential passengers for adding to a flight
        /// </summary>
        /// <param name="id">The unique name of the passenger</param>
        /// <returns>Http Status 200 on success.</returns>
        // GET: api/PassengersAPI/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(string id)
        {
            var response =  _repo.GetPassengers().FirstOrDefault(x=>x.Name==id);
            if (response == null) return BadRequest();
            return Ok(response);
        }
        
        // POST: api/PassengersAPI
        [HttpPost]
        public IActionResult Post([FromBody]PassengerDetails value)
        {
            if(ModelState.IsValid)
            {
                _repo.Add(value);
            }
            return Created("api/PassengersAPI/"+ value.Name,value);
        }
        
        // PUT: api/PassengersAPI/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _repo.Delete(id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public void Delete([FromBody]PassengerDetails pd)
        {
            _repo.Delete(pd);
        }
    }
}
