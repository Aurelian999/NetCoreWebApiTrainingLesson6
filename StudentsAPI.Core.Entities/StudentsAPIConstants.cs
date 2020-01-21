using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsAPI.Core.Entities
{

    public static class StudentsAPIResource
    {
        public const string APIName = "StudentAPI";
    }
    
    public static class StudentsAPIScopes
    {
        public const string Admin = "studentapi.admin";
        public const string User = "studentapi.readwrite";
        public const string RestrictedUser = "studentapi.readonly";
    }

}
