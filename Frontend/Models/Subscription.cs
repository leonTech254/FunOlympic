using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Models
{
    public class Subscription
    {
    public int Id { get; set; }
    public int UserId { get; set; }
    public int EventId { get; set; }
    public bool IsCancelled {get;set;}
    }
}