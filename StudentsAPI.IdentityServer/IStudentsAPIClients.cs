using IdentityServer4.Models;
using System.Collections.Generic;

namespace StudentsAPI.IdentityServer
{
    public interface IStudentsAPIClients
    {
        void Add(Client client);
        List<Client> GetAll();
    }
}