using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPIClient.Models
{
    public class StudentStatistics
    {
        public string Name { get; set; }
        public int Commits { get; set; }
        public long? Lines { get; set; }
    }

    public enum OrderBy
    {
        Name,
        Commits,
        Lines
    }

}
