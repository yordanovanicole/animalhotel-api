using animalhotelAPI.Data;
using animalhotelAPI.DTOs;
using animalhotelAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace animalhotelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public PetsController(HotelDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<PetReadDto>> Create(PetCreateDto dto)
        {
            if (!await _context.Owners.AnyAsync(o => o.Id == dto.OwnerId))
                return BadRequest("Owner not found");

            if (!await _context.PetTypes.AnyAsync(pt => pt.Id == dto.PetTypeId))
                return BadRequest("PetType not found");

            var pet = new Pet
            {
                Name = dto.Name,
                OwnerId = dto.OwnerId,
                PetTypeId = dto.PetTypeId
            };

            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();

            return Ok(new PetReadDto(pet.Id,pet.Name, pet.OwnerId,pet.PetTypeId));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetReadDto>>> GetAll()
        {
            var pets = await _context.Pets.Select(p => new PetReadDto(p.Id,p.Name,p.OwnerId, p.PetTypeId )).ToListAsync();
            return Ok(pets);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
                return NotFound();
            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
