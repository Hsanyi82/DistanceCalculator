using System;

class Program
{   
    // Store the average radius of Earth in kilometers.
    private const double EarthRadiusKm = 6371;

    // One nautical mile is exactly 1.852 kilometers.
    private const double KilometersPerNauticalMile = 1.852;

    // Create a list that can store Point objects.
    private List<Point> points = new List<Point>();

   
    private double CalculateHaversineDistance(double lat1, double lon1, double lat2, double lon2)
        {
            // Calculate the difference between the two latitudes in radians, because the Haversine formula requires angles in radians.
            var dLat = ToRadians(lat2 - lat1);

            // Calculate the difference between the two longitudes in radians.
            var dLon = ToRadians(lon2 - lon1);

            // Convert the first latitude from degrees to radians.
            lat1 = ToRadians(lat1);

            // Convert the second latitude from degrees to radians.
            lat2 = ToRadians(lat2);

            // Calculate the central angle between the two points, because the Haversine formula uses this angle to determine the great-circle distance.
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);

            // Convert the central angle into the angular distance.
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            // Multiply the angular distance by Earth's radius and return kilometers.
            return EarthRadiusKm * c;
        }

        // Convert an angle from degrees to radians.
        private double ToRadians(double angle) => Math.PI * angle / 180.0;

    // Convert a distance from kilometers to nautical miles.
    private double KilometersToNauticalMiles(double kilometers) => kilometers / KilometersPerNauticalMile;

    static void Main(string[] args)
    {
        // Create the first point from its latitude and longitude.
        Point p1 = new Point(47.873939888580836, 19.40219215883569);

        // Create the second point from its latitude and longitude.
        Point p2 = new Point(53.430894532245226, -2.9608328792539895);

        // Create an instance of the Program class.
        Program program = new Program();

        // Calculate the distance between the two points using the Haversine formula.
        double distance = program.CalculateHaversineDistance(p1.lat, p1.lon, p2.lat, p2.lon);

        // Convert the calculated distance to nautical miles.
        double distanceInNauticalMiles = program.KilometersToNauticalMiles(distance);

        // Display the calculated distance in the console.
        Console.WriteLine($"The distance between the points is: {distance} kilometers");
        Console.WriteLine($"The distance between the points is: {distanceInNauticalMiles} nautical miles");
    
    }

}
