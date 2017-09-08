using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trippy.Services
{
    public class GeoLocService
    {
        private ILogger<GeoLocService> _logger;

        public GeoLocService(ILogger<GeoLocService> logger)
        {
            _logger = logger;
        }

        public async Task<GeoLocResult> GetLocAsync(string name)
        {
            var result = new GeoLocResult()
            {
                Success = false,
                Message = "Failed to get coordinates",
            };
        }
    }
}
