using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace StudentsAPI.IdentityServer
{
    [Authorize(Policy = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsManagementController : ControllerBase
    {
        private readonly IStudentsAPIClients clientsService;

        public ClientsManagementController(IStudentsAPIClients clients)
        {
            this.clientsService = clients;
        }

        [HttpPost]
        public ActionResult CreateClient([FromQuery]string id, [FromQuery]string secret, [FromQuery] string[] scopes)
        {
            try
            {
                Client client = new Client()
                {
                    ClientId = id,

                    ClientSecrets = {
                    new Secret(secret.Sha256())
                },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = scopes
                };

                clientsService.Add(client);
            }
            catch (Exception)
            {
                return BadRequest("Invalid request. Client was not created");
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteClient(/*[FromQuery]*/string id)
        {
            Client client = clientsService.GetAll().Where(c => c.ClientId == id).FirstOrDefault();
            if (client == null)
            {
                return NotFound($"Client with id={id} was not found!");
            }

            clientsService.Remove(client);

            return Ok();
        }

    }
}
