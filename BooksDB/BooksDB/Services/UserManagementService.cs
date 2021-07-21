using BooksDB.Interfaces;
using BooksDB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BooksDB.Services
{
    class UserManagementService : IUserManagement, IDisposable
    {
        private const string BaseUrl = "http://10.0.2.2:61408/api/Users/";
        private const string MediaType = "application/json";

        private HttpClientHandler httpClientHandler;

        private readonly HttpClient client;

        private static UserManagementService instance = null;
        public static UserManagementService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserManagementService();
                }
                return instance;
            }
        }

        private UserManagementService()
        {
            httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
            client.DefaultRequestHeaders.Add("User-Agent", "AgentSmith");
        }

        public async Task<bool> AddToLibrary(int userId, LibraryItem item)
        {
            var requestContent = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, MediaType);

            var postRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{BaseUrl}{userId}"),
                Content = requestContent
            };

            var response = await client.SendAsync(postRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<UserDetails> AuthenticateUserAsync(User user)
        {
            var response = await client.GetAsync($"{BaseUrl}{user.Username}/{user.Password}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserDetails>(json);
            }

            throw new Exception("API request failed");
        }

        public async Task<IEnumerable<LibraryItem>> GetUserLibraryAsync(int userId)
        {
            var response = await client.GetAsync($"{BaseUrl}{userId}/1/25");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LibraryItem[]>(json);
            }

            throw new Exception("API request failed");
        }

        public async Task<UserDetails> RegisterUserAsync(UserDetails user)
        {
            var requestContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, MediaType);

            var postRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{BaseUrl}"),
                Content = requestContent
            };

            var response = await client.SendAsync(postRequest);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserDetails>(json);
            }

            return null;
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
