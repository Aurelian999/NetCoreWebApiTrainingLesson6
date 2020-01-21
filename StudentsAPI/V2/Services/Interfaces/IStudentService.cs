using StudentsAPI.V2.Models;
using System.Collections.Generic;

namespace StudentsAPI.V2.Services.Interfaces
{
    public interface IStudentsService
    {
        IEnumerable<Student> Get(Filter filter = null);
        void Add(Student student);
        void Update(Student student);
        void Patch(Student student);
        void Delete(long studentId);
    }
}
