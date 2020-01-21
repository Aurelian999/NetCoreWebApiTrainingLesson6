using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using StudentsAPI.V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace StudentsAPI.IntegrationTests
{
    public class StudentsControllerTests : IntegrationTestsBase
    {
        private readonly string accessToken;

        public StudentsControllerTests(StudentsApiWebApplicationFactory<Startup> factory) : base(factory)
        {
            client.DefaultRequestHeaders.Remove("x-api-key");
        }

        private async Task Authorize()
        {
            var tokenReq = GetToken();
            var token = await tokenReq;

            client.DefaultRequestHeaders.Add("authorization", "Bearer " + token.access_token);
        }

       [Fact]
       async Task TestGetStudents()
        {
            var response = await client.GetAsync("api/students");
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            string message = await response.Content.ReadAsStringAsync();
            Assert.Equal("Invalid ApiKey", message);
        }


        [Fact]
        async Task TestStudentsCount()
        {
            await Authorize();

            client.DefaultRequestHeaders.Add("x-api-key", "123456");

            var response = await client.GetAsync("api/students");
            response.EnsureSuccessStatusCode();
            using var responseStream = await response.Content.ReadAsStreamAsync();
            List<Student> students = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Student>>(responseStream);
            Assert.Equal(5, students.Count);
        }

        private static async Task<AccessToken> GetToken()
        {
            string clientId = "StudentAPIAdmin";
            string clientSecret = "admin-password";
            string credentials = String.Format("{0}:{1}", clientId, clientSecret);

            using (var client = new HttpClient())
            {
                //Define Headers
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));

                //Prepare Request Body
                List<KeyValuePair<string, string>> requestData = new List<KeyValuePair<string, string>>();
                requestData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                requestData.Add(new KeyValuePair<string, string>("scope", "studentapi.admin"));

                FormUrlEncodedContent requestBody = new FormUrlEncodedContent(requestData);

                //Request Token
                var request = await client.PostAsync("https://localhost:5000/connect/token", requestBody);
                var response = await request.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AccessToken>(response);
            }
        }

        private class AccessToken
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public long expires_in { get; set; }
        }
    }
}
