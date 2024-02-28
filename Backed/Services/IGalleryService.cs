using System.Collections.Generic;
using System.Threading.Tasks;
using Backed.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backed.Services
{
    public interface IGalleryService
    {
        Task<ActionResult<ResponseDTO>> GetGalleriesAsync();
        Task<ActionResult<ResponseDTO>> GetGalleryByIdAsync(int id);
        Task<ActionResult<ResponseDTO>> CreateGalleryAsync(GalleryDTO gallery);
        Task<ActionResult<ResponseDTO>> UpdateGalleryAsync(int id, Gallery gallery);
        Task<ActionResult<ResponseDTO>> DeleteGalleryAsync(int id);
    }
}
