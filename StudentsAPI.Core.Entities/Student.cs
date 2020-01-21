using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudentsAPI.V2.Models
{
    public class Student
    {
        [Required]
        [JsonPropertyName("id")]
        public long? Id { get; set; }
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
    }
}
