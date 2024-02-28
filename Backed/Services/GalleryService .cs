using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backed.Models;
using DBconnection_namespace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backed.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly DBconn _context;

        public GalleryService(DBconn context)
        {
            _context = context;
        }

        public async Task<ActionResult<ResponseDTO>> GetGalleriesAsync()
        {
            var galleries = await _context.Galleries.ToListAsync();
            return new ResponseDTO { message = "Success", responseData = galleries };
        }

        public async Task<ActionResult<ResponseDTO>> GetGalleryByIdAsync(int id)
        {
            var gallery = await _context.Galleries.FindAsync(id);
            if (gallery == null)
            {
                return new ResponseDTO { message = "Gallery not found", responseData = null };
            }
            return new ResponseDTO { message = "Success", responseData = gallery };
        }

        public async Task<ActionResult<ResponseDTO>> CreateGalleryAsync(GalleryDTO galleryDTO)
        {
            int userId=12; //from the token
            Gallery gallery= new Gallery(){
                PictureUrl=galleryDTO.PictureUrl,
                UserId=userId,
            };

            _context.Galleries.Add(gallery);
            await _context.SaveChangesAsync();
            return new ResponseDTO { message = "Gallery created successfully", responseData = gallery };
        }

        public async Task<ActionResult<ResponseDTO>> UpdateGalleryAsync(int id, Gallery gallery)
        {
            if (id != gallery.Id)
            {
                return new ResponseDTO { message = "Invalid gallery ID", responseData = null };
            }

            _context.Entry(gallery).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new ResponseDTO { message = "Gallery updated successfully", responseData = gallery };
        }

        public async Task<ActionResult<ResponseDTO>> DeleteGalleryAsync(int id)
        {
            var gallery = await _context.Galleries.FindAsync(id);
            if (gallery == null)
            {
                return new ResponseDTO { message = "Gallery not found", responseData = null };
            }

            _context.Galleries.Remove(gallery);
            await _context.SaveChangesAsync();
            return new ResponseDTO { message = "Gallery deleted successfully", responseData = null };
        }
    }
}
