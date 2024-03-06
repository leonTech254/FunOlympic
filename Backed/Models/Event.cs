using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backed.Models
{
   public class Event
{
     [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime EventDate { get; set; }

    public string playerName {get;set;}
    public string EventName { get; set; }
    public string EventImageUrl { get; set; }
    public string EventDescription { get; set; }

   //  [InverseProperty("Event")]
   //  public ICollection<EventSubscribers> EventSubscribers { get; set; }
}
}