using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using StudentsAPI.V2.Services;
using StudentsAPI.V2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsAPI.IntegrationTests
{
    public class StudentsApiWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureServices(c =>
            {
                c.AddSingleton<IStudentsGenerator, FixedNamesStudentsGenerator>();
            });
        }

    }
}
