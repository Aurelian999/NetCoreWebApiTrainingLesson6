using StudentsAPI.V2.Models;
using StudentsAPI.V2.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace StudentsAPI.V2.Services
{
    public class StudentsGenerator : IStudentsGenerator
    {
        public IEnumerable<Student> GenerateStudents(int studentsNumber)
        {
            List<Student> result = new List<Student>();

            for (int i = 1; i <= studentsNumber; i++)
            {
                string firstName = GenerateText(3);

                Student student = new Student()
                {
                    Id = i,
                    FirstName = firstName,
                    LastName = GenerateText(4),
                    Email = firstName + "@localhost",
                    Phone = "+ 40 0" + i + " 000"
                };

                result.Add(student);
            }
            return result;
        }

        private string GenerateText(int length)
        {
            Random random = new Random();
            string result = "";
            for (int i = 1; i <= length; i++)
            {
                char firstChar = i == 1 ? 'A' : 'a';
                int charIndex = firstChar + random.Next(26);
                result += (char)charIndex;
            }
            return result;
        }
    }
}
