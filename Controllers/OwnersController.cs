using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using animalhotelAPI.Data;
using animalhotelAPI.Models;
using animalhotelAPI.DTOs;

namespace animalhotelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnersController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public OwnersController(HotelDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var owners = await _context.Owners.Select(o => new OwnerReadDto(o.Id,o.FullName)).ToListAsync();
            return Ok(owners);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
           return NotFound();

           return Ok(new OwnerReadDto(owner.Id, owner.FullName));
        }

        [HttpPost]
        public async Task<IActionResult> Create(OwnerCreateDto dto)
        {
            var owner = new Owner
            {
                FullName = dto.FullName
            };

            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();

            return Ok(new OwnerReadDto(owner.Id,owner.FullName));

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var owner = await _context.Owners.FindAsync(id);

            if (owner == null)
                return NotFound("Owner not found");

            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
