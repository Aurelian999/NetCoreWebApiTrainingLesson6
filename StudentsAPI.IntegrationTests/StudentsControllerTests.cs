using Microsoft.AspNetCore.Mvc.Testing;
using StudentsAPI.V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace StudentsAPI.IntegrationTests
{
    public class StudentsControllerTests : IntegrationTestsBase
    {
        public StudentsControllerTests(StudentsApiWebApplicationFactory<Startup> factory) : base(factory)
        {
            client.DefaultRequestHeaders.Remove("x-api-key");
        }

       [Fact]
       async Task TestGetStudents()
        {
            var response = await client.GetAsync("api/students");
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            string message = await response.Content.ReadAsStringAsync();
            Assert.Equal("Invalid ApiKey", message);
            //response.EnsureSuccessStatusCode();
            //using var responseStream = await response.Content.ReadAsStreamAsync();
            //List<Student> students = await JsonSerializer.DeserializeAsync<List<Student>>(responseStream);
            //Assert.Equal(5, students.Count);
            //Assert.Equal("Test1", students.First().FirstName);

        }


    }
}
