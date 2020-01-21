using System.ComponentModel.DataAnnotations;

namespace StudentsAPI.V1.Models
{
    public class Student
    {
        [Required]
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
