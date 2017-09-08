using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace trippy.Services
{
    public class GeoLocService
    {
        private ILogger<GeoLocService> _logger;
        private IConfigurationRoot _config;

        public GeoLocService(ILogger<GeoLocService> logger, IConfigurationRoot config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task<GeoLocResult> GetLocAsync(string name)
        {
            var result = new GeoLocResult()
            {
                Success = false,
                Message = "Failed to get coordinates",
            };

            var apiKkey = _config["APIKeys:GoogleMapsAPI"];
            var encodedName = WebUtility.UrlEncode(name);
            var url = string.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}", encodedName, apiKkey);

            var client = new HttpClient();
            var json = await client.GetStringAsync(url);

            // Read out the results

            var results = JObject.Parse(json);
            var resources = results["results"][0]["geometry"];
            if (!resources.HasValues)
            {
                result.Message = $"Could not find '{name}' as a location";
            }
            else
            {
                var coords = resources["location"];
                result.Latitude = (double)coords["lat"];
                result.Longitude = (double)coords["lon"];
                result.Success = true;
                result.Message = "Success";
            }
            return result;
        }
    }
}
