namespace EventCampaignManagement.Models;

public class Event
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    
    /// <summary>
    /// Extra Implementation of item 7
    /// </summary>
    public decimal Price { get; set; }
    public DateTime Date { get; set; }

    public Event(int id, string name, string city, decimal price, DateTime date)
    {
        this.Id = id;
        this.Name = name;
        this.City = city;
        this.Price = price;
        this.Date = date;
    }

}

public class EventWithDistanceReference
{
    public double DistanceWithCustomer { get; set; }
    public Event Event { get; set; }
}