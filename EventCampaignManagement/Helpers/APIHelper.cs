using System.Net;
using System.Text.Json.Serialization;
using EventCampaignManagement.Models;
using Newtonsoft.Json;

namespace EventCampaignManagement.Helpers;

public class APIHelper
{
    /// <summary>
    /// Get the cities from a file
    /// </summary>
    /// <param name="cityNames"></param>
    /// <returns></returns>
    public static Dictionary<string, GPSCoordinate> GetAllUSStateCordinatesFromCache(List<string> cityNames)
    {
        var citiesPath = Path.Combine("Files", "USCities.json");
        
        //Check for file existence
        if (!File.Exists(citiesPath))
            return new Dictionary<string, GPSCoordinate>();
        
        var cities = JsonConvert.DeserializeObject<List<CityParser>>(File.ReadAllText(citiesPath));
        if (cities is not { })
            return new Dictionary<string, GPSCoordinate>();
        
        var result = new Dictionary<string, GPSCoordinate>();
        //Filter and Deserialize
        
        //Apply distinct to remove duplicate keys which will cause a crash. 
        //Get only the needed cities as performance measure. Needed cities are determined by availability in any event.
        cities.Where(__=> cityNames.Contains(__.Name)).DistinctBy(d=>d.Name).ToList().ForEach(_=>  result.Add(_.Name, new GPSCoordinate(_.Latitude, _.Longitude)));
        return result;
    }

    /// <summary>
    /// Simulate a failed API call that timed out. Further exceptions are caught in the catch and handled. 
    /// </summary>
    /// <returns></returns>
    public static (bool, Dictionary<string, GPSCoordinate> ) GetAllUSStateCordinatesFromAPI()
    {
        // Set the timeout for the request
        int timeout = 1000; // 1 second

        try
        {
            // Create a web request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.andelalabs.com");
            request.Timeout = timeout;

            // Try to get the response from the server
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        
            if (response.StatusCode != HttpStatusCode.OK)
            {
                //An Error occured.
                return (false, new Dictionary<string, GPSCoordinate>());
            }
        }
        catch (TimeoutException e)
        {
            Console.WriteLine(e);
            return (false, new Dictionary<string, GPSCoordinate>());
        }
        catch (WebException e)
        {
            Console.WriteLine(e);
            return (false, new Dictionary<string, GPSCoordinate>());
        }
        return (false, new Dictionary<string, GPSCoordinate>());
    }
}
