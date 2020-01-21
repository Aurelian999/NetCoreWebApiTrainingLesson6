using System.Text.Json.Serialization;

namespace StudentsAPI.V2.Models
{
    public class Filter
    {
        public Field Field { get; set; }
        public FilterType Type { get; set; }

        public string[] Values { get; set; }   
    }

    public enum FilterType
    {
        None,
        Equals,
        Contains,
        StartsWith,
        EndsWith
    }

    public enum Field
    {
        Name,
        Email,
        Phone
    }
}
