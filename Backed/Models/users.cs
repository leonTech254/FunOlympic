using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backed.Models;

public class User
{
     [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string country {get;set;}
    public string Password { get; set; }
    public string Role { get; set; }

    [InverseProperty("User")]
    public ICollection<Gallery> Galleries { get; set; }

    [InverseProperty("User")]
    public ICollection<EventSubscribers> EventSubscribers { get; set; }
}