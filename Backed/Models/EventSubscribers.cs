using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backed.Models
{
 public class EventSubscribers
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int UserId { get; set; }
    public int EventId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }

    [ForeignKey("EventId")]
    public Event Event { get; set; }
}
}