using System.Text.Json.Serialization;

namespace StudentsAPI.V1.Models
{
    public class Filter
    {
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
}
