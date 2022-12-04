namespace EventCampaignManagement.Models;

public class DistanceComparer
{
    public string CityA { get; set; }
    public string CityB { get; set; }

    public GPSCoordinate CityALocation { get; set; }
    public GPSCoordinate CityBLocation { get; set; }

    /// <summary>
    /// Distance in Kilometers
    /// </summary>
    public double Distance { get; set; }
}