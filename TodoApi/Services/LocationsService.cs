using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Services
{
    public class LocationsService : ILocationsService
    {
        private readonly KnownLocationsService _service;

        public LocationsService(KnownLocationsService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<Location>> GetLocations(Location location, int maxDistance, int maxResults)
        {
            // If there is no data, load -> Only occurs in the first request
            // There are other ways to warmup a singleton service, but I used this for simplicity
            if (_service.KnownLocations == null || _service.KnownLocations.Count() == 0)
            {
                _service.Load();
            }

            return await Task.FromResult(_service.KnownLocations.Where(a => a.CalculateDistance(location) <= maxDistance).Take(maxResults).ToList());
        }
    }
}
