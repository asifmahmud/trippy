using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trippy.Models;
using trippy.ViewModels;

namespace trippy.Controllers.Api
{
    public class TripsController : Controller
    {
        private IWorldRepository _repository;
        private WorldContext _context;

        public TripsController(IWorldRepository repository, WorldContext context)
        {
            _repository = repository;
            _context = context;
        }

        [HttpGet("api/trips")]
        public IActionResult Get()
        {

            try
            {
                var result = _repository.GetAllTrips();
                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(result));
            }
            catch(Exception ex)
            {
                string Message = string.Format("Something went wrong: {0}", ex.Message);
                return BadRequest(Message);
            }
        }

        [HttpPost("api/trips")]
        public IActionResult Post([FromBody]TripViewModel trip)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newTrip = Mapper.Map<Trip>(trip);
                    string path = string.Format("api/trips/{0}", trip.Name);
                    return Created(path, Mapper.Map<TripViewModel>(newTrip));
                }
                return BadRequest("Bad Data");
            }
            catch(Exception ex)
            {
                string Message = string.Format("Something went wrong: {0}", ex.Message);
                return BadRequest(Message);
            }
        }
    }
}
