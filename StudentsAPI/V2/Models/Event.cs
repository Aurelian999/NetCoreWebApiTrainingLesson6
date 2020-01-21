namespace StudentsAPI.V2.Models
{
    public class Event
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public int StatusCode { get; set; }
    }
}
