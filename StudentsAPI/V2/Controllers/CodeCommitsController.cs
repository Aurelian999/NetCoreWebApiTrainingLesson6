using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Core.Entities;
using StudentsAPI.V2.Services.Interfaces;

namespace StudentsAPI.V2.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class CodeCommitsController : ControllerBase
    {
        private readonly ICodeCommitsService codeCommitsService;
        private readonly IStudentsService studentsService;

        public CodeCommitsController(ICodeCommitsService codeCommitsService, IStudentsService studentsService)
        {
            this.codeCommitsService = codeCommitsService;
            this.studentsService = studentsService;
        }
        
        // GET: api/CodeCommits
        [HttpGet]
        [Authorize]
        public IEnumerable<CodeCommit> Get()
        {
            return codeCommitsService.Get();
        }

         // POST: api/CodeCommits
        [HttpPost]
        [Authorize(Policy = "RegularUser")]
        public ActionResult<CodeCommit> Post([FromBody] CodeCommit commit)
        {
            if (!studentsService.Get().Any(s => s.Id == commit.UserId))
            {
                return BadRequest();
            }
            codeCommitsService.Add(commit);
            return CreatedAtAction(nameof(Get), null, commit);
        }

    }
}
