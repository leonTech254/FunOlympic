using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backed.Models
{
  public class Comments
{
     [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int PictureId { get; set; }
    public string Comment { get; set; }

    [ForeignKey("PictureId")]
    public Gallery Gallery { get; set; }
}
}