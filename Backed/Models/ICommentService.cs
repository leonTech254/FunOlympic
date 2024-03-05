using System.Threading.Tasks;
using Backed.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backed.Services
{
    public interface ICommentService
    {
        Task<ActionResult<ResponseDTO>> GetCommentsAsync();
        Task<ActionResult<ResponseDTO>> GetCommentByIdAsync(int id);
        Task<ActionResult<ResponseDTO>> CreateCommentAsync(CommentsDTO comment,string jwttoken);
        Task<ActionResult<ResponseDTO>> UpdateCommentAsync(int id, Comments comment);
        Task<ActionResult<ResponseDTO>> DeleteCommentAsync(int id);
    }
}
