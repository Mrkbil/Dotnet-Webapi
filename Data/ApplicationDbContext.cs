using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Task.Models;

namespace Task.Data
{
    public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
        }

        public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
}
}




