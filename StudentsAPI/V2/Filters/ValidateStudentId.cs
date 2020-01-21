using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StudentsAPI.V2.Models;
using StudentsAPI.V2.Services.Interfaces;
using System.Linq;

namespace StudentsAPI.V2.Filters
{
    public class ValidateStudentId : IActionFilter
    {
        private readonly IStudentsService studentsService;

        public ValidateStudentId (IStudentsService studentsService)
        {
            this.studentsService = studentsService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.ContainsKey("id"))
            {
                return;
            }

            long id = (long)context.ActionArguments["id"];

            if (!studentsService.Get().Any(s => s.Id == id))
            {
                context.Result = new NotFoundResult();
            }

            if (!context.ActionArguments.ContainsKey("student"))
            {
                return;
            }

            Student student = (Student)context.ActionArguments["student"];

            if (student.Id != id)
            {
                context.Result = new BadRequestResult();
            }

        }
    }
}
