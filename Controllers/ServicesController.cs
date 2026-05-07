using animalhotelAPI.Data;
using animalhotelAPI.DTOs;
using animalhotelAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace animalhotelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public ServicesController(HotelDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceReadDto>> Create(ServiceCreateDto dto)
        {
            var service = new Service
            {
                Name = dto.Name
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return Ok(new ServiceReadDto(service.Id, service.Name));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceReadDto>>> GetAll()
        {
            var services = await _context.Services
                .Select(s => new ServiceReadDto(s.Id,s.Name)).ToListAsync();
            return Ok(services);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
                return NotFound();

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
