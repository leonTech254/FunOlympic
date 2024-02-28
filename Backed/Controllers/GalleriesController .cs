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
    [Route("/api/v1/galleries")]
    public class GalleriesController : ControllerBase
    {
        private readonly IGalleryService _galleryService;

        public GalleriesController(IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetGalleries()
        {
            return await _galleryService.GetGalleriesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetGallery(int id)
        {
            return await _galleryService.GetGalleryByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> CreateGallery(GalleryDTO gallery)
        {
            return await _galleryService.CreateGalleryAsync(gallery);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDTO>> UpdateGallery(int id, Gallery gallery)
        {
            return await _galleryService.UpdateGalleryAsync(id, gallery);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDTO>> DeleteGallery(int id)
        {
            return await _galleryService.DeleteGalleryAsync(id);
        }
    }
    }
