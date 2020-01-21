using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentsAPI.V2.Filters;
using StudentsAPI.V2.Models;
using StudentsAPI.V2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentsAPI.V2.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ValidateStudentId]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase
    {

        private readonly IStudentsService studentsService;
        private readonly ILogger<StudentsController> logger;

        public StudentsController(IStudentsService studentsService, ILogger<StudentsController> logger)
        {
            this.studentsService = studentsService;
            this.logger = logger;
        }

        // GET: api/Students
        [HttpGet]
        public IEnumerable<Student> Get([FromQuery] Filter filter)
        {
            return studentsService.Get(filter).ToList();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public ActionResult<Student> Get(long id)
        {
            return studentsService.Get().Single(s => s.Id == id);
        }

        // POST: api/Students
        [HttpPost]
        [Authorize(Policy = "RegularUser")]
        public ActionResult Post([FromBody] Student student)
        {
            if (studentsService.Get().Any(s => s.Id == student.Id))
            {
                studentsService.Update(student);
                return Ok(student);
            }

            studentsService.Add(student);
            return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        [Authorize(Policy = "RegularUser")]
        public ActionResult Put(long id, [FromBody] Student student)
        {
            studentsService.Update(student);
            return Ok(student);
        }

        // PATCH: api/Students
        [HttpPatch]
        [Authorize(Policy = "RegularUser")]
        public ActionResult<Student> SimplePatch([FromBody] Student student)
        {
            if (!studentsService.Get().Any(s => s.Id == student.Id))
            {
                return NotFound();
            }

            studentsService.Patch(student);
            Student patchedStudent = studentsService.Get().Single(s => s.Id == student.Id);
            return Ok(patchedStudent);
        }

        // PATCH: api/Students/5
        [HttpPatch("{id}")]
        [Authorize(Policy = "RegularUser")]
        public ActionResult<Student> Pacth(long id, [FromBody] JsonPatchDocument<Student> patchData)
        {
            Student student = studentsService.Get().Single(s => s.Id == id);
            patchData.ApplyTo(student, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "RegularUser")]
        public ActionResult Delete(long id)
        {
            studentsService.Delete(id);
            return Ok();
        }
    }
}
