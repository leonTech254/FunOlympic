using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backed.Models;
using Backed.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backed.Controllers
{
   
    [ApiController]
    [Route("/api/v1/comments")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetComments()
        {
            return await _commentService.GetCommentsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetComment(int id)
        {
            return await _commentService.GetCommentByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> CreateComment(CommentsDTO comment)
        {
            string jwtToken = HttpContext.Request.Headers["Authorization"];
            

            return await _commentService.CreateCommentAsync(comment,jwtToken);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDTO>> UpdateComment(int id, Comments comment)
        {
            return await _commentService.UpdateCommentAsync(id, comment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDTO>> DeleteComment(int id)
        {
            return await _commentService.DeleteCommentAsync(id);
        }
    }
        
    }
