using System;
using System.Threading.Tasks;
using Backed.Models;
using DBconnection_namespace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backed.Services
{
    public class CommentService : ICommentService
    {
        private readonly DBconn _context;

        public CommentService(DBconn context)
        {
            _context = context;
        }

        public async Task<ActionResult<ResponseDTO>> GetCommentsAsync()
        {
            var comments = await _context.Comments.ToListAsync();
            return new ResponseDTO { message = "Success", responseData = comments };
        }

        public async Task<ActionResult<ResponseDTO>> GetCommentByIdAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return new ResponseDTO { message = "Comment not found", responseData = null };
            }
            return new ResponseDTO { message = "Success", responseData = comment };
        }

        public async Task<ActionResult<ResponseDTO>> CreateCommentAsync(CommentsDTO commentsDTO)
        {
            Comments comment= new()
            {
                Comment=commentsDTO.Comment,
                PictureId=commentsDTO.PictureId
                


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
