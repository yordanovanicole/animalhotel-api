using animalhotelAPI.Data;
using animalhotelAPI.DTOs;
using animalhotelAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace animalhotelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public EmployeesController(HotelDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeReadDto>> Create(EmployeeCreateDto dto)
        {
            var employee = new Employee
            {
                FullName = dto.FullName
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return Ok(new EmployeeReadDto(employee.Id, employee.FullName));
        }

        [HttpGet] 
        public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> GetAll()
        {
            var employees = await _context.Employees.Select(e => new EmployeeReadDto( e.Id, e.FullName)).ToListAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeReadDto>> GetById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            return Ok(new EmployeeReadDto(employee.Id, employee.FullName));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
