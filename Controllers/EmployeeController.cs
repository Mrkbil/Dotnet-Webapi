using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task.Models;
using Task.Data;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _context.Employees.ToListAsync();
            return Ok(employees);
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // GET: api/Employee/byName?name=John
        [HttpGet("byName")]
        public async Task<IActionResult> GetEmployeeByName(string name)
        {
            var employees = await _context.Employees
                .Where(e => e.Name.Contains(name))
                .ToListAsync();

            if (employees == null || !employees.Any())
            {
                return NotFound();
            }

            return Ok(employees);
        }

        // GET: api/Employee/byPhone?phone=1234567890
        [HttpGet("byPhone")]
        public async Task<IActionResult> GetEmployeeByPhone(string phone)
        {
            var employees = await _context.Employees
                .Where(e => e.PhoneNumber == phone)
                .ToListAsync();

            if (employees == null || !employees.Any())
            {
                return NotFound();
            }

            return Ok(employees);
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Employees.Any(e => e.Id == id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
