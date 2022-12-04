using System.Text.Json.Serialization;

namespace EventCampaignManagement.Models;

public class CityParser
{
    [JsonPropertyName("lat")]
    public double Latitude { get; set; }
    
    [JsonPropertyName("lng")]
    public double Longitude { get; set; }
    
    [JsonPropertyName("city")]
    public string Name { get; set; }
}