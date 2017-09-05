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

        public TripsController(IWorldRepository repository)
        {
            _repository = repository;
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
    }
}
