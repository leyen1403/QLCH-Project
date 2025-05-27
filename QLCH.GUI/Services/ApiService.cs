using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.GUI.Services
{
    public class ApiService
    {
        private readonly HttpClient _client;

        public ApiService()
        {
            var apiBaseAddress = ConfigurationManager.AppSettings["ApiBaseAddress"];
            _client = new HttpClient
            {
                BaseAddress = new Uri(apiBaseAddress)
            };
        }

        public async Task<List<T>> GetListAsync<T>(string endpoint)
        {
            var response = await _client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<T>>(json);

            return result ?? new List<T>();
        }

        public async Task<T> GetByIdAsync<T>(string endpoint)
        {
            var response = await _client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(json);

            if (result == null)
                throw new Exception("Phản hồi không hợp lệ từ API.");

            return result;
        }

        public async Task<bool> PostAsync<T>(string endpoint, T data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(endpoint, content);
            return response.IsSuccessStatusCode;
        }
    }
}
