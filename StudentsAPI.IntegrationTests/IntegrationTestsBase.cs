using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using Xunit;

namespace StudentsAPI.IntegrationTests
{
    public class IntegrationTestsBase : IClassFixture<StudentsApiWebApplicationFactory<Startup>>
    {
        protected readonly StudentsApiWebApplicationFactory<Startup> factory;
        protected readonly HttpClient client;

        public IntegrationTestsBase(StudentsApiWebApplicationFactory<Startup> factory)
        {
            client = factory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001/");
            client.DefaultRequestHeaders.Add("api-version", "2.0");
            client.DefaultRequestHeaders.Add("x-api-key", "123456");
            this.factory = factory;
        }

    }
}
