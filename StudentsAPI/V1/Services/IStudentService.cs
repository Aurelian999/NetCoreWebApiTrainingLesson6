using StudentsAPI.V1.Models;
using System.Collections.Generic;

namespace StudentsAPI.V1.Services
{
    public interface IStudentsService
    {
        IEnumerable<Student> Get(Filter filter = null);
        void Add(Student student);
        void Update(Student student);
        void Delete(long studentId);
    }
}
