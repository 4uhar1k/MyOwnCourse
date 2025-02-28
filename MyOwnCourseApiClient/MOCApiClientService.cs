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
    }
}
