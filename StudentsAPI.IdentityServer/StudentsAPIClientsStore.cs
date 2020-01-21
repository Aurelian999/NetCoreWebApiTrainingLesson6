using IdentityServer4.Models;
using IdentityServer4.Stores;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPI.IdentityServer
{
    public class StudentsAPIClientsStore : IClientStore
    {
        private readonly IStudentsAPIClients clients;

        public StudentsAPIClientsStore(IStudentsAPIClients clients)
        {
            this.clients = clients;
        }


        public Task<Client> FindClientByIdAsync(string clientId)
        {
            return Task.FromResult(clients.GetAll().SingleOrDefault(c => c.ClientId == clientId));
        }
    }
}