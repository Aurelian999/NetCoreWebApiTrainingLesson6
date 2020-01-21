using IdentityModel.Client;
using StudentsAPIClient.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StudentsAPIClient.Services
{
    public class AccessTokenService
    {
        private readonly HttpClient client;
        private readonly IAccessTokenStore accessTokenStore;
        private readonly IClientCredentials clientCredentials;

        public AccessTokenService(HttpClient client, IAccessTokenStore accessTokenStore, IClientCredentials clientCredentials)
        {
            this.accessTokenStore = accessTokenStore;
            this.clientCredentials = clientCredentials;
            this.client = client;
            client.BaseAddress = new Uri("https://localhost:5000/");
        }

        public async Task<string> GetAccessToken()
        {
            if (accessTokenStore.AccessToken != null)
            {
                return accessTokenStore.AccessToken;
            }

            return await GetNewAccessToken();
        }

        public async Task<string> GetNewAccessToken()
        {
            var discoveryDocument = await client.GetDiscoveryDocumentAsync();
            if (discoveryDocument.IsError)
            {
                throw new AccessTokenException(discoveryDocument.Error);
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = clientCredentials.ClientId,
                ClientSecret = clientCredentials.ClientSecret,

                Scope = clientCredentials.Scope
            });

            if (tokenResponse.IsError)
            {
                throw new AccessTokenException(tokenResponse.Error);
            }

            accessTokenStore.AccessToken = tokenResponse.AccessToken;
            return tokenResponse.AccessToken;
        }
    }
}
