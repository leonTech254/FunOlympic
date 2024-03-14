using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Frontend.Models;

namespace Frontend.Services
{
    public class DummyDataService
    {
        public Task<List<EventModel>> GetPastEvents()
        {
            List<EventModel> pastEvents = new List<EventModel>();

            // Add dummy past Olympic events
            for (int i = 1; i <= 25; i++)
            {
                string sport;
                string imageUrl;
                switch (i % 5)
                {
                    case 1:
                        sport = "Football";
                        imageUrl="https://img.freepik.com/free-photo/success-grass-soccer-ball-generated-by-ai_188544-9819.jpg";
                        break;
                    case 2:
                        sport = "Hockey";
                        imageUrl="https://img.freepik.com/free-photo/hockey-players_654080-1960.jpg";
                        break;
                    case 3:
                        sport = "Basketball";
                        imageUrl="https://img.freepik.com/free-photo/basketball-game-concept_23-2150910692.jpg";

                        break;
                    case 4:
                        sport = "Relay race";
                        imageUrl="https://media.wired.com/photos/5932be46b44176024420ed48/master/pass/Kerron-Stewart.jpg";
                        break;
                    default:
                        sport = "Athletics";
                        imageUrl="https://www.fisu.net/app/uploads/2023/08/athletics-1-1440x650.jpg";
                        break;
                }

                pastEvents.Add(new EventModel
                {
                    Id = i,
                    EventDate = DateTime.Now.AddMonths(-i), // Dummy past dates
                    PlayerName = $"Player {i}",
                    EventName = $"Olympic {sport} Event {i}",
                    EventImageUrl =imageUrl, //"https://img.freepik.com/free-photo/basketball-game-concept_23-2150910692.jpg", // Placeholder image URL
                    EventDescription = $"Description for Olympic {sport} Event {i}",
                    EventVenue = $"Venue for Olympic {sport} Event {i}"
                });
            }

            return Task.FromResult(pastEvents);
        }
    }
}
