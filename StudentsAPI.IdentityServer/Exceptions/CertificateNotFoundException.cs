using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPI.IdentityServer.Exceptions
{
    public class CertificateNotFoundException : Exception
    {
        public CertificateNotFoundException(string message) : base(message)
        {
        }
    }
}
