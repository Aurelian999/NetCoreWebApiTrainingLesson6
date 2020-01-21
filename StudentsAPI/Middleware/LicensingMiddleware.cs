using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPI.Middleware
{
    public class LicensingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IConfiguration configuration;

        public LicensingMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this.next = next;
            this.configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers["x-api-key"] != configuration.GetValue<string>("ApiKey"))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Invalid ApiKey");
                return;
            }
            await next(context);
        }
    }
}