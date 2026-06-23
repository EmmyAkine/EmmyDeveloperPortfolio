using EmmyDeveloperPortfolio.Data;
using EmmyDeveloperPortfolio.DTO;
using EmmyDeveloperPortfolio.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmmyDeveloperPortfolio.Controllers.Api {
    [ApiController]
    [Route("api/[Controller]")]
    public class GalleryApiController : ControllerBase {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public GalleryApiController(AppDbContext context, IWebHostEnvironment env) {
            _context = context;
            _env = env;
        }

        //POST /api/gallery/upload
        [HttpPost("upload")]
        public async Task<IActionResult> Gallery([FromForm] GalleryUploadDto dto) {
            if (dto.Image == null || dto.Image.Length == 0) {
                return BadRequest("Image is required");
            }

            // Build a unique filename so two uploads don't overwrite each other
            var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create)) {
                await dto.Image.CopyToAsync(stream);
            }

            // Save the record to the database
            var galleryItem = new GalleryItem {
                Title = dto.Title,
                ImagePath = "/uploads/" + fileName
            };

            _context.GalleryItems.Add(galleryItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = galleryItem?.Id }, galleryItem);
        }


        //GET /api/gallery
        [HttpGet]
        public IActionResult GetAll() {
            var items = _context.GalleryItems.OrderByDescending(g => g.UploadedAt).ToList();
            if (items == null || items.Count <= 0) {
                return NoContent();
            }
            return Ok(items);
        }

        //GET /api/gallery/{id} 
        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var item = _context.GalleryItems.FirstOrDefault(g => g.Id == id);
            if (item == null) {
                return NotFound(new { message = $"Gallery item with id {id} was not found" });
            }
            return Ok(item);
        }
    }
}
