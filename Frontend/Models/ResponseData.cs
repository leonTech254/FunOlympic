using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Models
{
    public class ResponseData
    {
    public List<UserModel> Users { get; set; }
    public List<CommentModel> Comments { get; set; }
    }
}