using StudentsAPI.V2.Models;
using System.Collections.Generic;

namespace StudentsAPI.V2.Services.Interfaces
{
    public interface IStudentsGenerator
    {
        IEnumerable<Student> GenerateStudents(int studentsNumber);
    }
}