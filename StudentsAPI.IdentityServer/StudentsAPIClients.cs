using IdentityServer4.Models;
using StudentsAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPI.IdentityServer
{
    public class StudentsAPIClients : IStudentsAPIClients
    {
        private readonly List<Client> clients;

        public StudentsAPIClients()
        {
            this.clients = new List<Client>
            {
                new Client()
                {
                    ClientId = "StudentAPIAdmin",

                    ClientSecrets = {
                        new Secret("admin-password".Sha256())
                    },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    AllowedScopes =
                    {
                        StudentsAPIScopes.Admin, StudentsAPIScopes.User, StudentsAPIScopes.RestrictedUser
                    }
                }
            };
        }

        public List<Client> GetAll()
        {
            return clients;
        }

        public void Add(Client client)
        {
            lock (clients)
            {
                clients.Add(client);
            };
        }

    }
}
