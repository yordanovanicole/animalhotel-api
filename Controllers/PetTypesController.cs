using animalhotelAPI.Data;
using animalhotelAPI.DTOs;
using animalhotelAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace animalhotelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetTypesController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public PetTypesController(HotelDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<PetTypeReadDto>> Create(PetTypeCreateDto dto)
        {
            var petType = new PetType
            {
                Name = dto.Name
            };

            _context.PetTypes.Add(petType);
            await _context.SaveChangesAsync();

            return Ok(new PetTypeReadDto(petType.Id, petType.Name));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetTypeReadDto>>> GetAll()
        {
            var petTypes = await _context.PetTypes.Select(pt => new PetTypeReadDto(pt.Id, pt.Name)).ToListAsync();
            return Ok(petTypes);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var petType = await _context.PetTypes.FindAsync(id);

            if (petType == null)
                return NotFound();

            _context.PetTypes.Remove(petType);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
