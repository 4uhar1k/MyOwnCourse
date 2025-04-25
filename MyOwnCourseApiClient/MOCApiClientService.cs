using MyOwnCourseApiClient.Models;
using MyOwnCourseApiClient.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MyOwnCourseApiClient
{
    public class MOCApiClientService
    {
        private readonly HttpClient _httpClient;

        public MOCApiClientService(ApiClientOptions apiClientOptions)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(apiClientOptions.ApiBaseAdress);
        }

        public async Task<List<User>?> GetUsers()
        {
            return await _httpClient.GetFromJsonAsync<List<User>?>("/api/User");
        }

        public async Task<User?> GetUserById(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<User?>($"/api/User/id/{id}");
            }
            catch
            {
                return null;
            }            
        }

        public async Task<User?> GetUserByLoginNPassword(string login, string password)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<User?>($"/api/User/login/{login}/password/{password}");
            }
            catch
            {
                return null;
            }

        }
        public async Task PostUser(User user)
        {
            await _httpClient.PostAsJsonAsync("/api/User", user);
        }
        public async Task PutUser(User user)
        {
            await _httpClient.PutAsJsonAsync("/api/User", user);
        }
        public async Task DeleteUser(int id)
        {
            await _httpClient.DeleteAsync($"/api/User/{id}");
        }

        public async Task<List<Course>?> GetCourses()
        {
            return await _httpClient.GetFromJsonAsync<List<Course>?>("/api/Course");
        }

        public async Task<Course?> GetCourseById(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Course?>($"/api/Course/id/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<Course?> GetCourseByCategory(string category)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Course?>($"/api/Course/category/{category}");
            }
            catch
            {
                return null;
            }

        }
        public async Task PostCourse(Course course)
        {
            await _httpClient.PostAsJsonAsync("/api/Course", course);
        }
        public async Task PutCourse(User course)
        {
            await _httpClient.PutAsJsonAsync("/api/Course", course);
        }
        public async Task DeleteCourse(int id)
        {
            await _httpClient.DeleteAsync($"/api/Course/{id}");
        }

        public async Task<List<Role>?> GetRoles()
        {
            return await _httpClient.GetFromJsonAsync<List<Role>?>("/api/Role");
        }

        public async Task<List<Connection>?> GetConnections()
        {
            return await _httpClient.GetFromJsonAsync<List<Connection>?>("/api/Connection");
        }
        public async Task<List<Connection>?> GetConnectionsByCourseId(int CourseId)
        {
            return await _httpClient.GetFromJsonAsync<List<Connection>?>($"/api/Connection/courseid/{CourseId}");
        }

        public async Task CreateConnection(Connection connection)
        {
            await _httpClient.PostAsJsonAsync("/api/Connection", connection);
        }
    }
}
