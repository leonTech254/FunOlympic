namespace Frontend.Models
{
    public class EventModel
    {
    public int Id { get; set; }
    public DateTime EventDate { get; set; }
    public string EventName { get; set; }
    public string EventImageUrl { get; set; }
    public string EventDescription { get; set; }
    public string PlayerName {get;set;}
        
    }
}