//using ZWLDotNetCore.ConsoleAppHttpClientExample;

//HttpClientExample client = new HttpClientExample();
//await client.RunAsync();

//Console.ReadLine();

HttpClient client = new HttpClient();

var response = await client.GetAsync("https://localhost:7121/api/blog");
if (response.IsSuccessStatusCode)
{
    string jsonStr = await response.Content.ReadAsStringAsync();
    Console.WriteLine(jsonStr);
}