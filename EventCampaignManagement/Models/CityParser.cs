using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace EventCampaignManagement.Models;

public class CityParser
{
    [JsonProperty("lat")]
    public double Latitude { get; set; }
    
    [JsonProperty("lng")]
    public double Longitude { get; set; }
    
    [JsonProperty("city")]
    public string Name { get; set; }
}