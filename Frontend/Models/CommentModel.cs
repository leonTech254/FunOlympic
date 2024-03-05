using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
    public int PictureId { get; set; }
    public string Comment { get; set; }
    public string username {get;set;}

    public string userId {get;set;}
    }
}