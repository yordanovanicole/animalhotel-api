using animalhotelAPI.Data;
using animalhotelAPI.DTOs;
using animalhotelAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace animalhotelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExtrasController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public ExtrasController(HotelDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<ExtraReadDto>> Create(ExtraCreateDto dto)
        {
            var extra = new Extra
            {
                Name = dto.Name
            };

            _context.Extras.Add(extra);
            await _context.SaveChangesAsync();

            return Ok(new ExtraReadDto(extra.Id, extra.Name));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExtraReadDto>>> GetAll()
        {
            var extras = await _context.Extras.Select(e => new ExtraReadDto(e.Id,e.Name)).ToListAsync();
            return Ok(extras);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExtraReadDto>> GetById(int id)
        {
            var extra = await _context.Extras.FindAsync(id);

            if (extra == null)
                return NotFound();

            return Ok(new ExtraReadDto(extra.Id, extra.Name));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var extra = await _context.Extras.FindAsync(id);

            if (extra == null)
                return NotFound();

            _context.Extras.Remove(extra);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
