using StudentsAPI.V2.Models;
using StudentsAPI.V2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentsAPI.V2.Services
{
    public class StudentsService : IStudentsService
    {

        private readonly List<Student> students;
        private readonly IStudentsGenerator studentsGenerator;

        public StudentsService(IStudentsGenerator studentsGenerator)
        {
            students = new List<Student>();
            this.studentsGenerator = studentsGenerator;
            Initialize();
        }

        public void Add(Student student)
        {
            lock (students)
            {
                students.Add(student);
            }
        }

        public void Delete(long studentId)
        {
            lock (students)
            {
                students.RemoveAll(s => s.Id == studentId);
            }
        }

        public IEnumerable<Student> Get(Filter filter = null)
        {
            if (filter == null || filter.Values == null || filter.Type == FilterType.None)
            {
                return students;
            }

            IEnumerable<Student> result = new List<Student>();

            foreach (var value in filter.Values)
            {
                switch (filter.Type)
                {
                    case FilterType.Equals:
                        result = result.Union(students.Where(s => MatchEquals(s, value, filter.Field)));
                        break;
                    case FilterType.Contains:
                        result = result.Union(students.Where(s => MatchContains(s, value, filter.Field)));
                        break;
                    case FilterType.StartsWith:
                        result = result.Union(students.Where(s => MatchStartsWith(s, value, filter.Field)));
                        break;
                    case FilterType.EndsWith:
                        result = result.Union(students.Where(s => MatchEndsWith(s, value, filter.Field)));
                        break;
                }
            }

            return result;  
        }

        private bool MatchEquals(Student s, string value, Field field)
        {
            return field switch
            {
                Field.Email => s.Email.Equals(value, StringComparison.CurrentCultureIgnoreCase),
                Field.Phone => s.Phone.Equals(value, StringComparison.CurrentCultureIgnoreCase),
                _ => (s.FirstName + " " + s.LastName).Equals(value, StringComparison.CurrentCultureIgnoreCase),
            };
        }

        private bool MatchStartsWith(Student s, string value, Field field)
        {
            return field switch
            {
                Field.Email => s.Email.StartsWith(value, StringComparison.CurrentCultureIgnoreCase),
                Field.Phone => s.Phone.StartsWith(value, StringComparison.CurrentCultureIgnoreCase),
                _ => (s.FirstName + " " + s.LastName).StartsWith(value, StringComparison.CurrentCultureIgnoreCase),
            };
        }

        private bool MatchContains(Student s, string value, Field field)
        {
            return field switch
            {
                Field.Email => s.Email.Contains(value, StringComparison.CurrentCultureIgnoreCase),
                Field.Phone => s.Phone.Contains(value, StringComparison.CurrentCultureIgnoreCase),
                _ => (s.FirstName + " " + s.LastName).Contains(value, StringComparison.CurrentCultureIgnoreCase),
            };
        }

        private bool MatchEndsWith(Student s, string value, Field field)
        {
            return field switch
            {
                Field.Email => s.Email.EndsWith(value, StringComparison.CurrentCultureIgnoreCase),
                Field.Phone => s.Phone.EndsWith(value, StringComparison.CurrentCultureIgnoreCase),
                _ => (s.FirstName + " " + s.LastName).EndsWith(value, StringComparison.CurrentCultureIgnoreCase),
            };
        }

        public void Update(Student student)
        {
            lock (students)
            {
                Student studentToUpdate = students.Single(s => s.Id == student.Id);
                studentToUpdate.FirstName = student.FirstName;
                studentToUpdate.LastName = student.LastName;
                studentToUpdate.Email = student.Email;
                studentToUpdate.Phone = student.Phone;
            }
        }

        public void Patch(Student student)
        {
            lock (students)
            {
                Student studentToUpdate = students.Single(s => s.Id == student.Id);
                studentToUpdate.FirstName = student.FirstName ?? studentToUpdate.FirstName;
                studentToUpdate.LastName = student.LastName ?? studentToUpdate.LastName;
                studentToUpdate.Email = student.Email ?? studentToUpdate.Email;
                studentToUpdate.Phone = student.Phone ?? studentToUpdate.Phone;
            }
        }

        private void Initialize()
        {
            students.AddRange(studentsGenerator.GenerateStudents(5));
        }
    }
}
