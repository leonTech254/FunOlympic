using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backed.Models
{
  public class Gallery
{
     [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int UserId { get; set; }
    public string PictureUrl { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }

    // [InverseProperty("Gallery")]
    // public ICollection<Comments> Comments { get; set; }
}

}