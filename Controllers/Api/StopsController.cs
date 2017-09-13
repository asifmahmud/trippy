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
using trippy.Services;
using trippy.ViewModels;

namespace trippy.Controllers.Api
{
    [Authorize]
    public class StopsController : Controller
    {
        private ILogger<StopsController> _logger;
        private IWorldRepository _repository;
        private GeoLocService _geoloc;

        public StopsController(IWorldRepository repository, GeoLocService geoloc, ILogger<StopsController> logger)
        {
            _logger = logger;
            _repository = repository;
            _geoloc = geoloc;
        }

        [HttpGet("api/trips/{tripName}/stops")]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetUserTripByName(tripName, User.Identity.Name);
                var stops = trip.Stops.OrderBy(stop => stop.Order).ToList();
                return Ok(Mapper.Map<IEnumerable<StopViewModel>>(stops));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get stops: {0}", ex);
            }
            return BadRequest("An error occured while trying to get stops");
        }

        [HttpPost("api/trips/{tripName}/stops")]
        public async Task<IActionResult> Post(string tripName, [FromBody] StopViewModel stop)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(stop);

                    var geocode = await _geoloc.GetLocAsync(newStop.Name);

                    if (!geocode.Success)
                    {
                        _logger.LogError(geocode.Message);
                    }
                    else
                    {
                        newStop.Latitude = geocode.Latitude;
                        newStop.Longitude = geocode.Longitude;

                        _repository.AddStop(tripName, newStop, User.Identity.Name);
                        if (await _repository.SaveChangesAsync())
                        {
                            string path = string.Format("api/trips/{0}/stops/{1}", tripName, newStop.Name);
                            return Created(path, Mapper.Map<StopViewModel>(newStop));
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to save stops: {0}", ex);

            }
            return BadRequest("An error occured while trying to save stops");

        }
    }
}
