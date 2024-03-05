using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backed.Models;
using Backed.Utills;
using DBconnection_namespace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backed.Services
{
    // public class CommentResponse
    // {
       
    //     public Comments Comments { get; set; }
    // }

    public class CommentService : ICommentService
    {
        private readonly DBconn _context;
        private readonly Jwt _jwt;
        

        public CommentService(DBconn context, Jwt jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        public async Task<ActionResult<ResponseDTO>> GetCommentsAsync()
        {
            var comments = await _context.Comments.ToListAsync();
            return new ResponseDTO { message = "Success", responseData = comments };
        }

public async Task<ActionResult<ResponseDTO>> GetCommentByIdAsync(int id)
{
    var allusers = await _context.Users.ToListAsync();
    var allComments = await _context.Comments.Where(c => c.PictureId == id).ToListAsync();

    List<object> allcommentsWithUsers = new List<object>(); // Moved outside the loop
    var usersCommented=new List<User>();
    var commentList=new List<Comments>();

    foreach (var comment in allComments)
    {
        List<User> userList = new List<User>();
        Console.WriteLine("hello leon");
         usersCommented.Add(allusers.Find(u => u.Id == int.Parse(comment.userId)));
         commentList.Add(comment);
        allcommentsWithUsers.Add(new { comment, userList });
    }

    if (allComments == null || allComments.Count == 0) // Check for empty list
    {
        return new ResponseDTO { message = "Comment not found", responseData = null };
    }
    return new ResponseDTO { message = "Success", responseData = new {users = usersCommented,comment=commentList} };
}



        public async Task<ActionResult<ResponseDTO>> CreateCommentAsync(CommentsDTO commentsDTO, string jwtToken)
        {
            string userId = _jwt.GetUserIdFromToken(jwtToken);

            Comments comment = new Comments
            {
                Comment = commentsDTO.Comment,
                PictureId = commentsDTO.PictureId,
                userId = userId
            };
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return new ResponseDTO { message = "Comment created successfully", responseData = comment };
        }

        public async Task<ActionResult<ResponseDTO>> UpdateCommentAsync(int id, Comments comment)
        {
            if (id != comment.Id)
            {
                return new ResponseDTO { message = "Invalid comment ID", responseData = null };
            }

            _context.Entry(comment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new ResponseDTO { message = "Comment updated successfully", responseData = comment };
        }

        public async Task<ActionResult<ResponseDTO>> DeleteCommentAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return new ResponseDTO { message = "Comment not found", responseData = null };
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return new ResponseDTO { message = "Comment deleted successfully", responseData = null };
        }
    }
}
