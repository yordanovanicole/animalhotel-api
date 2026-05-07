using animalhotelAPI.Data;
using animalhotelAPI.DTOs;
using animalhotelAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace animalhotelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public RoomsController(HotelDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<RoomReadDto>> Create(RoomCreateDto dto)
        {
            var room = new Room
            {
                RoomNumber = dto.RoomNumber
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return Ok(new RoomReadDto(room.Id,room.RoomNumber));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomReadDto>>> GetAll()
        {
            var rooms = await _context.Rooms.Select(r => new RoomReadDto(r.Id,r.RoomNumber)).ToListAsync();
            return Ok(rooms);
        }
    }
}
