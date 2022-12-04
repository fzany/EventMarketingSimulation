namespace EventCampaignManagement.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public DateTime BirthDate { get; set; }
    
    public decimal WalletBalance { get; set; }

}