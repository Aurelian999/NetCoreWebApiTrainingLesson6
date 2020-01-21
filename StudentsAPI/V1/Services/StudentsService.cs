using StudentsAPI.V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using IStudentsServiceV2 = StudentsAPI.V2.Services.Interfaces.IStudentsService;
using StudentV2 = StudentsAPI.V2.Models.Student;
using FilterV2 = StudentsAPI.V2.Models.Filter;
using AutoMapper;

namespace StudentsAPI.V1.Services
{
    public class StudentsService : IStudentsService
    {

        private readonly IStudentsServiceV2 studentsService;
        private readonly IMapper mapper;

        public StudentsService(IStudentsServiceV2 studentsService, IMapper mapper)
        {
            this.studentsService = studentsService;
            this.mapper = mapper;
        }

        public void Add(Student student)
        {
            StudentV2 studentV2 = mapper.Map<StudentV2>(student);
            studentsService.Add(studentV2);
        }

        public void Delete(long studentId)
        {
            studentsService.Delete(studentId);
        }

        public IEnumerable<Student> Get(Filter filter = null)
        {
            FilterV2 filterV2 = null;

            if (filter != null) {
                filterV2 = mapper.Map<FilterV2>(filter);
                filterV2.Field = V2.Models.Field.Name;
            }

            IEnumerable<StudentV2> result = studentsService.Get(filterV2);
            return mapper.Map<IEnumerable<Student>>(result).ToList();
        }

        public void Update(Student student)
        {
            StudentV2 studentV2 = mapper.Map<StudentV2>(student);
            studentsService.Patch(studentV2);
        }
    }
}
