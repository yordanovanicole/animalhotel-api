using animalhotelAPI.Data;
using animalhotelAPI.DTOs;
using animalhotelAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace animalhotelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public ReservationsController(HotelDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<ReservationReadDto>> Create(ReservationCreateDto dto)
        {
            if (!await _context.Pets.AnyAsync(p => p.Id == dto.PetId))
                return BadRequest("Pet not found");

            if (!await _context.Rooms.AnyAsync(r => r.Id == dto.RoomId))
                return BadRequest("Room not found");

            if (!await _context.Employees.AnyAsync(e => e.Id == dto.EmployeeId))
                return BadRequest("Employee not found");

            var reservation = new Reservation
            {
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                PetId = dto.PetId,
                RoomId = dto.RoomId,
                EmployeeId = dto.EmployeeId
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return Ok(new ReservationReadDto(reservation.Id,reservation.StartDate,reservation.EndDate,
        reservation.PetId, reservation.RoomId,reservation.EmployeeId));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationReadDto>>> GetAll()
        {
            var reservations = await _context.Reservations.Select(r => new ReservationReadDto(r.Id,r.StartDate,r.EndDate,r.PetId,r.RoomId,r.EmployeeId)).ToListAsync();
            return Ok(reservations);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
                return NotFound();

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
