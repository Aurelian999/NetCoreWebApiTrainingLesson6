using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace StudentsAPIClient.Services
{
    public class AuthenticationDelegatingHandler : DelegatingHandler
    {
        private readonly AccessTokenService accessTokenService;

        public AuthenticationDelegatingHandler(AccessTokenService accessTokenService)
        {
            this.accessTokenService = accessTokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await accessTokenService.GetAccessToken();
            request.SetBearerToken(token);

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                token = await accessTokenService.GetNewAccessToken();
                request.SetBearerToken(token);
                response = await base.SendAsync(request, cancellationToken);
            }

            return response;

        }
    }
}
