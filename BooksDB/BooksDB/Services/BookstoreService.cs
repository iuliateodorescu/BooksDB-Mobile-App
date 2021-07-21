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
    class BookstoreService : IBookstore, IDisposable
    {
        private const string BaseUrl = "http://10.0.2.2:61408/api/Books/";
        private const string MediaType = "application/json";

        private HttpClientHandler httpClientHandler;

        private readonly HttpClient client;

        private static BookstoreService instance = null;
        public static BookstoreService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BookstoreService();
                }
                return instance;
            }
        }

        private BookstoreService()
        {
            httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
            client.DefaultRequestHeaders.Add("User-Agent", "AgentSmith");
        }

        public async Task<Book> GetBookAsync(int bookId)
        {
            var response = await client.GetAsync($"{BaseUrl}{bookId}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Book>(json);
            }

            throw new Exception("API request failed");
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            var response = await client.GetAsync($"{BaseUrl}1/25");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Book[]>(json);
            }
            
            throw new Exception("API request failed");
        }

        public async Task<IEnumerable<Book>> GetBooksByTitleAsync(string text)
        {
            var response = await client.GetAsync($"{BaseUrl}{text}/1/25");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Book[]>(json);
            }

            throw new Exception("API request failed");
        }

        public async Task<BookDetails> GetDetailsAsync(int bookId)
        {
            var response = await client.GetAsync($"{BaseUrl}{bookId}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BookDetails>(json);
            }

            throw new Exception("API request failed");
        }

        public void Dispose()
        {
            client.Dispose();
        }

        public async Task<IEnumerable<Book>> GetRecommendations(string title)
        {
            var response = await client.GetAsync($"{BaseUrl}getRecommendations/{title}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var b = JsonConvert.DeserializeObject<List<Book>>(json);
                return b;
            }

            throw new Exception("API request failed");
        }
    }
}
