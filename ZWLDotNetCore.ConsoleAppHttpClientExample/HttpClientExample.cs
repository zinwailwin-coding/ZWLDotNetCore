using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ZWLDotNetCore.ConsoleAppHttpClientExample
{
    internal class HttpClientExample
    {
        private readonly HttpClient client = new HttpClient() { BaseAddress = new Uri("https://localhost:7121") };
        private readonly string _blogEndpoint = "api/blog";
        public async Task RunAsync()
        {
            //await ReadAsync();
            //await EditAsync(1);
            //await EditAsync(1000);
            //await DeleteAsync(1002);
            //await CreateAsync("pont", "test pont", "testpont");
            //await UpdateAsync(4005,"pont 1", "test pont 1", "testpont 1");
            await EditAsync(4005);
        }

        public async Task ReadAsync()
        {           
            var response = await client.GetAsync(_blogEndpoint);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);
                List<BlogDto> result = JsonConvert.DeserializeObject<List<BlogDto>>(jsonStr)!;
                foreach (var blog in result)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(blog));
                    Console.WriteLine($"{blog.BlogTitle}");
                    Console.WriteLine($"{blog.BlogAuthor}");
                    Console.WriteLine($"{blog.BlogContent}");
                }
            }
           
        }

        public async Task EditAsync(int id)
        {
            var response = await client.GetAsync($"{_blogEndpoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                BlogDto blog = JsonConvert.DeserializeObject<BlogDto>(jsonStr)!;               
                Console.WriteLine(blog);
                Console.WriteLine($"{blog.BlogTitle}");
                Console.WriteLine($"{blog.BlogAuthor}");
                Console.WriteLine($"{blog.BlogContent}");                
            }
            else
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);
            }

        }

        public async Task CreateAsync(string title, string author, string content)
        {
            BlogDto model = new BlogDto()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            string jsonStr= JsonConvert.SerializeObject(model);
            HttpContent httpContent = new StringContent(jsonStr,Encoding.UTF8,Application.Json);
            var response = await client.PostAsync(_blogEndpoint,httpContent);
            if (response.IsSuccessStatusCode) {
                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
            }
        }

        public async Task UpdateAsync(int id,string title, string author, string content)
        {
            BlogDto model = new BlogDto()
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            string jsonStr = JsonConvert.SerializeObject(model);
            HttpContent httpContent = new StringContent(jsonStr, Encoding.UTF8, Application.Json);
            var response = await client.PutAsync($"{_blogEndpoint}/{id}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var response = await client.DeleteAsync($"{_blogEndpoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);
            }
            else
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);
            }

        }
    }
}
