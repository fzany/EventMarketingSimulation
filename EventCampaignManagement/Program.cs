// See https://aka.ms/new-console-template for more information

using EventCampaignManagement.Helpers;
using EventCampaignManagement.Models;
using EventCampaignManagement.Services;

Dictionary<string, GPSCoordinate> AllUsCitiesStore = new Dictionary<string, GPSCoordinate>();
Dictionary<Tuple<string, string>, double>
    DistanceComparerStore = new Dictionary<Tuple<string, string>, double>();

#region Property Seed

//Initialize events with random or given values. 
var events = new List<Event>{
    new Event(1, "Phantom of the Opera", "New York", 50, new DateTime(2023,12,23)),
    new Event(2, "Metallica", "Los Angeles", 60, new DateTime(2023,12,02)),
    new Event(3, "Metallica", "New York",70, new DateTime(2023,12,06)),
    new Event(4, "Metallica", "Boston", 55,new DateTime(2023,10,23)),
    new Event(5, "LadyGaGa", "New York", 45,new DateTime(2023,09,20)),
    new Event(6, "LadyGaGa", "Boston", 104,new DateTime(2023,08,01)),
    new Event(7, "LadyGaGa", "Chicago",20, new DateTime(2023,07,04)),
    new Event(8, "LadyGaGa", "San Francisco",44, new DateTime(2023,07,07)),
    new Event(9, "LadyGaGa", "Washington", 60,new DateTime(2023,05,22)),
    new Event(10, "Metallica", "Chicago", 10,new DateTime(2023,01,01)),
    new Event(11, "Phantom of the Opera", "San Francisco", 0,new DateTime(2023,07,04)),
    new Event(12, "Phantom of the Opera", "Chicago", 5,new DateTime(2024,05,15))
};

var customer = new Customer()
{
    Id = 1,
    Name = "John",
    City = "New York",
    WalletBalance = 68,
    BirthDate = new DateTime(1995, 05, 10),
};

#endregion

#region Print events in the same City as Customer

var sameCityEvents = events.Where(d => d.City == customer.City).ToList();
var engine = new MarketingEngine(customer, sameCityEvents);
engine.SendCustomerNotifications();

#endregion

#region Print events that are happening close to Customer's birthday
var birthDayEvents = events.Where(d => Converters.IsDatesClose(d.Date, customer.BirthDate)).ToList();
engine = new MarketingEngine(customer, birthDayEvents);
engine.SendCustomerNotifications();
#endregion

#region Print 5 closest events to the Customer

//Select distinct City names.
var cityNames = events.Select(d => d.City).ToList();
//add the customer city
cityNames.Add(customer.City);
cityNames = cityNames.Distinct().ToList();

//Populate US Locations from API.
(bool, Dictionary<string, GPSCoordinate>) locationResult = APIHelper.GetAllUSStateCordinatesFromAPI();
//Populate the static city store if the list is blank OR the API call fails. 
if (!AllUsCitiesStore.Any())
{
    AllUsCitiesStore = APIHelper.GetAllUSStateCordinatesFromCache();
}

//Get only the needed cities as performance measure. Needed cities are determined by availability in any event.
var allUsCitiesStoreFiltered = AllUsCitiesStore.Where(d => cityNames.Contains(d.Key));


//for each unique city, get distance with Customer
var allUsCitiesStore = allUsCitiesStoreFiltered as KeyValuePair<string, GPSCoordinate>[] ?? allUsCitiesStoreFiltered.ToArray();
cityNames.ForEach(d =>
{
    if (d == customer.City)
    {
        DistanceComparerStore.Add(new Tuple<string, string>(d, customer.City), 0);
    }
    else
    {
        DistanceComparerStore.Add(new Tuple<string, string>(d,customer.City), Converters.DistanceBetweenCoordinates(
            allUsCitiesStore.FirstOrDefault(f => f.Key == d).Value,
            allUsCitiesStore.FirstOrDefault(f => f.Key == customer.City).Value));
    }
});


//Calculate closeness for all the cities wrt the customer location. Choose the closest 5. 
var eventExtras = new List<EventWithDistanceReference>();
events.ForEach(d=> eventExtras.Add(new EventWithDistanceReference()
{
    Event = d, DistanceWithCustomer = DistanceComparerStore.FirstOrDefault(e=>e.Key.Item1 == d.City).Value
}));
var closestEvents = eventExtras.OrderByDescending(d => d.DistanceWithCustomer).Take(5).Select(d=>d.Event).ToList();
//print closest events to the customer. 
engine = new MarketingEngine(customer, closestEvents);
engine.SendCustomerNotifications();

#endregion

#region Print events based on the price and the wallet value of the Customer

//calculate based on price... Print the 
var pricedEvents = events.Where(d=>d.Price <= customer.WalletBalance).OrderBy(d => d.Price).ToList();
//print. 
engine = new MarketingEngine(customer, pricedEvents);
engine.SendCustomerNotifications();


#endregion










