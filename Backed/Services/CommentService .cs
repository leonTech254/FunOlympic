using System;
using System.Threading.Tasks;
using Backed.Models;
using Backed.Utills;
using DBconnection_namespace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Backed.Services
{
    public class CommentService : ICommentService
    {
        private readonly DBconn _context;
        private  readonly Jwt _jwt;

        public CommentService(DBconn context,Jwt jwt)
        {
            _context = context;
            _jwt=jwt;
        }

        public async Task<ActionResult<ResponseDTO>> GetCommentsAsync()
        {
            var comments = await _context.Comments.ToListAsync();
            return new ResponseDTO { message = "Success", responseData = comments };
        }

        public async Task<ActionResult<ResponseDTO>> GetCommentByIdAsync(int id)
        {
            var comment = await _context.Comments.Where(e=>e.PictureId==id).ToListAsync();
            if (comment == null)
            {
                return new ResponseDTO { message = "Comment not found", responseData = null };
            }
            comment.Reverse();
            return new ResponseDTO { message = "Success", responseData = comment };
        }

        public async Task<ActionResult<ResponseDTO>> CreateCommentAsync(CommentsDTO commentsDTO,string jwtToken)
        {
           string userId=_jwt.GetUserIdFromToken(jwtToken);
           string username=_jwt.GetUsernameFromToken(jwtToken);

            Comments comment= new()
            {
                Comment=commentsDTO.Comment,
                PictureId=commentsDTO.PictureId,
                userId=userId,
                username=username

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
