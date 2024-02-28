using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backed.Models
{
    public class EventDTO
    {
        public int Id { get; set; }
        public DateTime EventDate { get; set; }
        public string EventName { get; set; }
        public string EventImageUrl { get; set; }
        public string EventDescription { get; set; }
    }
}