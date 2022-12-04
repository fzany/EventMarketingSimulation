using EventCampaignManagement.Models;

namespace EventCampaignManagement.Helpers;

public class Converters
{
    public static bool IsDatesClose(DateTime eventDate, DateTime birthDate)
    {
        return Math.Abs(eventDate.DayOfYear - birthDate.DayOfYear) <= Constants.DateCloseRange || 
               //handle for next birthday. 
               Math.Abs(eventDate.DayOfYear - birthDate.AddYears(1).DayOfYear) <= Constants.DateCloseRange;
    }
    
    public static bool IsDistancesClose(GPSCoordinate eventCity, GPSCoordinate customerCity)
    {
        var distance = DistanceBetweenCoordinates(eventCity, customerCity);
        return distance <= Constants.DistanceCloseRange;
    }
    
    // Calculate the distance between two GPS coordinates in Kilometers
    public static double DistanceBetweenCoordinates(GPSCoordinate eventCity, GPSCoordinate customerCity)
    {
        var earthRadius = 6371e3; // meters
        var lat1 = eventCity.Latitude * Math.PI / 180;
        var lat2 = customerCity.Latitude * Math.PI / 180;
        var latDelta = (customerCity.Latitude - eventCity.Latitude) * Math.PI / 180;
        var lonDelta = (customerCity.Longitude - eventCity.Longitude) * Math.PI / 180;

        var a = Math.Sin(latDelta / 2) * Math.Sin(latDelta / 2) +
                Math.Cos(lat1) * Math.Cos(lat2) *
                Math.Sin(lonDelta / 2) * Math.Sin(lonDelta / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return earthRadius * c / 1000;
    }
}