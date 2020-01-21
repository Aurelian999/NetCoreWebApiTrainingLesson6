using IdentityModel.Client;
using StudentsAPI.Core.Entities;
using StudentsAPI.V2.Models;
using StudentsAPIClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudentsAPIClient.Services
{
    public class StudentsAPIService
    {
        private readonly HttpClient client;

        public StudentsAPIService(HttpClient client)
        {
            this.client = client;
            client.BaseAddress = new Uri("https://localhost:5002/");
            client.DefaultRequestHeaders.Add("api-version", "2.0");
            client.DefaultRequestHeaders.Add("x-api-key", "123456");
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/students");

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                string message = string.Format(
                    "Error while using StudentsAPI (status code: {0}, reason: {1})",
                    response.StatusCode,
                    response.ReasonPhrase);

                throw new HttpRequestException(message);
            }

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<Student>>(responseStream);
        }

        public async Task<IEnumerable<CodeCommit>> GetCodeCommits()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/codecommits");

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                string message = string.Format(
                    "Error while using StudentsAPI (status code: {0}, reason: {1})",
                    response.StatusCode,
                    response.ReasonPhrase);

                throw new HttpRequestException(message);
            }

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<CodeCommit>>(responseStream);
        }

        public async Task<IEnumerable<StudentStatistics>> GetStudentsStatistics(OrderBy orderBy = OrderBy.Name)
        {
            var getStudentsTask = GetStudents();
            var getCommitsTask = GetCodeCommits();

            var result = new List<StudentStatistics>();
            var students = await getStudentsTask;
            var commits = await getCommitsTask;

            foreach (var student in students)
            {
                result.Add(new StudentStatistics()
                {
                    Name = student.FirstName + " " + student.LastName,
                    Commits = commits.Count(c => c.UserId == student.Id),
                    Lines = commits.Where(c => c.UserId == student.Id).Sum(c => c.LinesModified)
                });
            }

            return orderBy switch
            {
                OrderBy.Commits => result.OrderByDescending(ss => ss.Commits),
                OrderBy.Lines => result.OrderByDescending(ss => ss.Lines),
                _ => result.OrderBy(ss => ss.Name)
            };
        }
    }
}
