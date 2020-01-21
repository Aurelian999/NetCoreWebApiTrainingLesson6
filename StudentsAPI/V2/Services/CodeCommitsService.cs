using StudentsAPI.Core.Entities;
using StudentsAPI.V2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPI.V2.Services
{
    public class CodeCommitsService : ICodeCommitsService
    {
        private readonly List<CodeCommit> commits;

        public CodeCommitsService()
        {
            commits = new List<CodeCommit>();
            Initialize();
        }

        public void Add(CodeCommit commit)
        {
            lock (commits)
            {
                commits.Add(commit);
            }
        }

        public IEnumerable<CodeCommit> Get()
        {
            return commits;
        }

        private void Initialize()
        {
            commits.Add(new CodeCommit() { UserId = 1, Description = "Tema1", LinesModified = 24 });
            commits.Add(new CodeCommit() { UserId = 1, Description = "Tema2", LinesModified = 20 });
            commits.Add(new CodeCommit() { UserId = 2, Description = "Tema1", LinesModified = 34 });
            commits.Add(new CodeCommit() { UserId = 1, Description = "Tema3", LinesModified = 51 });
            commits.Add(new CodeCommit() { UserId = 3, Description = "Tema1", LinesModified = 7 });
            commits.Add(new CodeCommit() { UserId = 2, Description = "Tema2", LinesModified = 80 });
        }
    }
}
