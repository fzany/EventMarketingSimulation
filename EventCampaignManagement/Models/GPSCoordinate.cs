namespace EventCampaignManagement.Models;

public class GPSCoordinate
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public GPSCoordinate(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}