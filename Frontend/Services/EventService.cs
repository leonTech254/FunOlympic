using System;
using System.Collections.Generic;
using Frontend.Models; 
namespace Frontend.Services
{
    public class EventService
    {
        public List<EventModel> Events { get; private set; }

        public EventService()
        {
            // Initialize with some dummy data
            Events = new List<EventModel>
            {
                new EventModel
                {
                    EventDate = DateTime.Now.AddDays(1),
                    EventName = "Event 1",
                    EventImageUrl = "https://example.com/image1.jpg",
                    EventDescription = "Description of Event 1"
                },
                new EventModel
                {
                    EventDate = DateTime.Now.AddDays(2),
                    EventName = "Event 2",
                    EventImageUrl = "https://example.com/image2.jpg",
                    EventDescription = "Description of Event 2"
                },
                new EventModel
                {
                    EventDate = DateTime.Now.AddDays(3),
                    EventName = "Event 3",
                    EventImageUrl = "https://example.com/image3.jpg",
                    EventDescription = "Description of Event 3"
                }
            };
        }

        public void AddEvent(EventModel newEvent)
        {
            Events.Add(newEvent);
        }

        public void UpdateEvent(EventModel updatedEvent)
        {
            var existingEvent = Events.Find(e => e.EventDate == updatedEvent.EventDate);
            if (existingEvent != null)
            {
                existingEvent.EventName = updatedEvent.EventName;
                existingEvent.EventImageUrl = updatedEvent.EventImageUrl;
                existingEvent.EventDescription = updatedEvent.EventDescription;
            }
        }

        public void DeleteEvent(DateTime eventDate)
        {
            Events.RemoveAll(e => e.EventDate == eventDate);
        }
    }

    // public class EventModel
    // {
    //     public DateTime EventDate { get; set; }
    //     public string EventName { get; set; }
    //     public string EventImageUrl { get; set; }
    //     public string EventDescription { get; set; }
    // }
}
