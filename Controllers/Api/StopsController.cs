﻿using AutoMapper;
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
    public class StopsController : Controller
    {
        private ILogger<StopsController> _logger;
        private IWorldRepository _repository;

        public StopsController(IWorldRepository repository, ILogger<StopsController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("api/trips/{tripName}/stops")]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetTripByName(tripName);
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
                    _repository.AddStop(tripName, newStop);
                    if (await _repository.SaveChangesAsync())
                    {
                        string path = string.Format("api/trips/{0}/stops/{1}", tripName, newStop.Name);
                        return Created(path, Mapper.Map<StopViewModel>(newStop));
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
