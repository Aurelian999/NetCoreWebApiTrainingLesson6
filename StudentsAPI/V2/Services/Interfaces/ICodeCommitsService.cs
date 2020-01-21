using StudentsAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPI.V2.Services.Interfaces
{
    public interface ICodeCommitsService
    {
        IEnumerable<CodeCommit> Get();
        void Add(CodeCommit commit);
    }
}
