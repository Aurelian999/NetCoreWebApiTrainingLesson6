using IdentityServer4.Models;
using StudentsAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPI.IdentityServer
{
    public static class StudentsAPIResources
    {
        public static IEnumerable<ApiResource> Get()
        {
            return new List<ApiResource> {
                new ApiResource()
                {
                    Name = StudentsAPIResource.APIName,

                    Scopes =
                    {
                        new Scope(StudentsAPIScopes.Admin),
                        new Scope(StudentsAPIScopes.User),
                        new Scope(StudentsAPIScopes.RestrictedUser)
                    }
                }
            };
        }
    }
}
