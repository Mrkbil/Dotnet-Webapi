using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Task.Models;




namespace Task.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<Employee> Employee { get; set; }
    }
}
