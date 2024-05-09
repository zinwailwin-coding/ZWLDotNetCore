using Newtonsoft.Json;
using RestSharp;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ZWLDotNetCore.ConsoleAppRestClientExamples
{
    internal class RestClientExample
    {
        private readonly RestClient client = new RestClient(new Uri("https://localhost:7121"));
        private readonly string _blogEndpoint = "api/blog";
        public async Task RunAsync()
        {
            await ReadAsync();
            //await EditAsync(1);
            //await EditAsync(1000);
            //await DeleteAsync(1002);
            //await CreateAsync("aung zaw ", "test pont", "testpont");
            //await UpdateAsync(4006,"aung zaw 2", "aung pont", "testpont 1");
            //await EditAsync(4006);
        }

        public async Task ReadAsync()
        {
            RestRequest restRequest = new RestRequest(_blogEndpoint, Method.Get);
            var response = await client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {               
                string jsonStr = response.Content!;
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
            RestRequest restRequest = new RestRequest($"{_blogEndpoint}/{id}", Method.Get);
            var response = await client.GetAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                BlogDto blog = JsonConvert.DeserializeObject<BlogDto>(jsonStr)!;
                Console.WriteLine(blog);
                Console.WriteLine($"{blog.BlogTitle}");
                Console.WriteLine($"{blog.BlogAuthor}");
                Console.WriteLine($"{blog.BlogContent}");
            }
            else
            {
                string jsonStr = response.Content!;
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
            RestRequest restRequest = new RestRequest(_blogEndpoint, Method.Post);
            restRequest.AddJsonBody(model);
            var response = await client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content!;
                Console.WriteLine(result);
            }
        }

        public async Task UpdateAsync(int id, string title, string author, string content)
        {
            BlogDto model = new BlogDto()
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            RestRequest restRequest = new RestRequest($"{_blogEndpoint}/{id}", Method.Put);
            restRequest.AddJsonBody(model);
            var response = await client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content!;
                Console.WriteLine(result);
            }
        }

        public async Task DeleteAsync(int id)
        {
            RestRequest restRequest = new RestRequest($"{_blogEndpoint}/{id}", Method.Get);
            var response = await client.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                Console.WriteLine(jsonStr);
            }
            else
            {
                string jsonStr = response.Content!;
                Console.WriteLine(jsonStr);
            }
        }
    }
}
