using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Services
{
    public class KnownLocationsService
    {
        public IEnumerable<Location> KnownLocations { get; set; }

        private string csvFilePath { get; set; }

    
        public KnownLocationsService(string csvFilePath)
        {
            this.csvFilePath = csvFilePath;
        }

        // Upon initialization, first request, we want to load the csv inside a 'data structure' and keep it in memory
        // That is why it is a singleton
        // This could also be an interface communicating with a Redis cache for example, or memcached
        public void Load()
        {
            KnownLocations = new HashSet<Location>();

            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                var records = new List<Location>();
                while (csv.Read())
                {
                    var record = new Location
                    {
                        Address = csv.GetField<string>("Address"),
                        Latitude = csv.GetField<double>("Latitude"),
                        Longitude = csv.GetField<double>("Longitude")
                    };

                    records.Add(record);
                }

                KnownLocations = records.ToHashSet<Location>();
            }
        }
    }
}
