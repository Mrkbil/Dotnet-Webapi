using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Task.Models;
using Task.Data;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Department
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _context.Departments.ToListAsync();

            return Ok(departments);
        }

        // GET: api/Department/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        // GET: api/Department/byName?name=Sales
        [HttpGet("byName")]
        public async Task<IActionResult> GetDepartmentByName(string name)
        {
            var departments = await _context.Departments
                .Where(d => d.Name.Contains(name)).ToListAsync();

            if (departments == null || !departments.Any())
            {
                return NotFound();
            }

            return Ok(departments);
        }

        // POST: api/Department
        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartmentById), new { id = department.Id }, department);
        }

        // PUT: api/Department/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Departments.Any(d => d.Id == id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Department/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
