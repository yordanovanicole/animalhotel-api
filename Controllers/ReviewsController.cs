using animalhotelAPI.Data;
using animalhotelAPI.DTOs;
using animalhotelAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace animalhotelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public ReviewsController(HotelDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReviewCreateDto dto)
        {
            var reservationExists = await _context.Reservations
                .AnyAsync(r => r.Id == dto.ReservationId);

            if (!reservationExists)
                return BadRequest("Reservation not found");

            var review = new Review
            {
                Text = dto.Text,
                ReservationId = dto.ReservationId
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return Ok(new ReviewReadDto(
                review.Id,
                review.Text,
                review.ReservationId
            ));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _context.Reviews.Select(r => new ReviewReadDto(r.Id,r.Text,r.ReservationId)).ToListAsync();
            return Ok(reviews);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
                return NotFound();

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
