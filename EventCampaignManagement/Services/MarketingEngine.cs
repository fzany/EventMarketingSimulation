using EventCampaignManagement.Models;

namespace EventCampaignManagement.Services;

public class MarketingEngine
{
    private readonly Customer customer;
    private readonly List<Event> _events;

    public MarketingEngine(Customer customer, List<Event> events)
    {
        this.customer = customer;
        this._events = events;
    }


    public void SendCustomerNotifications()
    {
        foreach (var e in _events)
        {
            Console.WriteLine($"{customer.Name} from {customer.City} event {e.Name} at {e.Date}");
        }
    }
}