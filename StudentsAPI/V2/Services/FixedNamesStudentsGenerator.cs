using StudentsAPI.V2.Models;
using StudentsAPI.V2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPI.V2.Services
{
    public class FixedNamesStudentsGenerator : IStudentsGenerator
    {
        public IEnumerable<Student> GenerateStudents(int studentsNumber)
        {

            List<Student> result = new List<Student>();

            for (int i = 1; i <= studentsNumber; i++)
            {
                Student student = new Student()
                {
                    Id = i,
                    FirstName = "Test" + i,
                    LastName = "Student" + i,
                    Email = "test" + i + "@localhost",
                    Phone = "+ 40 0" + i + " 000"
                };

                result.Add(student);
            }
            return result;
        }
    }
}
