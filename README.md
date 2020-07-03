### RoamlerChallenge

#### Description

Since the design and implementation details were left to my discretion, I have designed a minimal web API that fulfills the specification requirements.

The web API is based on ASP.NET Core 3.1, and is a simplistic environment where the endpoints can be accessed through simple HTTP requests (e.g. through Postman).


#### Getting started
The signature function (requested in the document)
``` C#
QueryResults GetLocations(Location location, int maxDistance, int maxResults);
```

Is fullfiled by the ```LocationsService.cs``` (depends also on ```KnownLocationsService```).

I have altered the return type to ```IEnumerable<Location>``` for simplicity.

You can either access the function by injecting the service in a controller of your choice, or hit the endpoint ```localhost:5002/api/locations``` via a GET request.

As a body you are to send a JSON type object containing the following (--example):

``` JSON
{
 "Location":
 {
   "Longitude": 2.0,
   "Latitude": 3.0,
   "Address": "YOLO"
 },
 "MaxDistance": 50,
 "MaxResults": 10
}
```

#### Implementation Details

1. The ```KnownLocationsService.cs``` is a "Singleton" service, with a sole functionality of loading up the CSV (given the appropiate path in the constructor) and keeping track of all known locations in-memory.
    1. This can easily be integrated to a caching mechanism (e.g. Redis cache)
    2. Track of Locations is kept in a HashSet, as several locations had the same Longitude, Latitude representing the same physical location
        1. This can be easily extended to hold multiple names with same coordinates
    3. The HashSet variant reduces total number of "CSV rows" and boosts performance
2. ```Location.cs``` has been extended to hold an optional Address field, and ```GetHashCode, Equals``` methods have been overriden to support HashSet comparisons.
3. ```LocationsService``` initially loads up the CSV when there are no data available. This _will_ induce a performance overhead in the first request. However, subsequent requests will be _much_ faster
4. Instead of using ```for-while-loops``` I am retrieving the results with a Linq query. This may or may not have a performance overhead, I have not tested the actual statement for performance validation. However, this allows for easily readable and maintainable code.


#### Remarks

Everything is designed with simplicity in mind based on the given facts (e.g. provided csv, specification, etc.). 

There will always be optimizations and fine-tuning to achieve optimal performance.
