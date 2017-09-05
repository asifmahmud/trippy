using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trippy.Models;

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
                return Ok(_repository.GetAllTrips());
            }
            catch(Exception ex)
            {
                string Message = string.Format("Something went wrong: {0}", ex.Message);
                return BadRequest(Message);
            }
        }

        [HttpPost("api/trips")]
        public IActionResult Post([FromBody]Trip trip)
        {
            try
            {
                return Ok(true);
                //return Ok(_context.Trips.Add(trip));
            }
            catch(Exception ex)
            {
                string Message = string.Format("Something went wrong: {0}", ex.Message);
                return BadRequest(Message);
            }
        }
    }
}
