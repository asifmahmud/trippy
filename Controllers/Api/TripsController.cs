using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trippy.Models;
using trippy.ViewModels;

namespace trippy.Controllers.Api
{
    [Authorize]
    public class TripsController : Controller
    {
        
        private IWorldRepository _repository;
        private ILogger<TripViewModel> _logger;

        public TripsController(IWorldRepository repository, ILogger<TripViewModel> logger)
        {
            _repository = repository;
            _logger = logger;
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
                _logger.LogError("Unable to get all trips: {0}", ex);
                return BadRequest("Error Occured while trying to get trip information");
            }
        }

        [HttpPost("api/trips")]
        public async Task<IActionResult> Post([FromBody]TripViewModel trip)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newTrip = Mapper.Map<Trip>(trip);
                    _repository.AddTrip(newTrip);
                    if (await _repository.SaveChangesAsync())
                    {
                        string path = string.Format("api/trips/{0}", trip.Name);
                        return Created(path, Mapper.Map<TripViewModel>(newTrip));
                    }
                    else
                    {
                        return BadRequest("Error while saving trip to database");
                    }
                }
                return BadRequest("Bad Data");
            }
            catch(Exception ex)
            {
                _logger.LogError("Unable to save trip data: {0}", ex);
                return BadRequest("Error Occured while trying to save trip information");
            }
        }
    }
}
