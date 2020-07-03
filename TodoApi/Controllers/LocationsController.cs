using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationsService locationsService;

        public LocationsController(ILocationsService lservice)
        {
            locationsService = lservice;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocations([FromBody] PostLocationQuery query)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var location = query.Location;
                var maxDist = query.MaxDistance;
                var maxResults = query.MaxResults;
                var results = await locationsService.GetLocations(location, maxDist, maxResults);

                if (results == null || results.Count() == 0)
                {
                    return NoContent();
                }

                return Ok(results);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
