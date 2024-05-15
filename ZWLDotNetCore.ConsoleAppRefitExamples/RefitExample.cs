using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZWLDotNetCore.ConsoleAppRefitExamples
{
    public class RefitExample
    {
        private readonly IBlogApi _service = RestService.For<IBlogApi>("https://localhost:7172");
        public async Task RunAsync()
        {
            //ReadAsync();
            //EditAsync(4006);
            //DeleteAsync(5007);
            //CreateAsync("pont", "pont", "pont");
            UpdateAsync(5006,"pont 1", "pont 2", "pont 3");
        }

        private async Task ReadAsync()
        {
            
            var lst = await _service.GetBlogs();
            foreach (var item in lst)
            {
                Console.WriteLine($"Id => {item.BlogId}");
                Console.WriteLine($"Title => {item.BlogTitle}");
                Console.WriteLine($"Author => {item.BlogAuthor}");
                Console.WriteLine($"Content => {item.BlogContent}");
            }
        }

        private async Task EditAsync(int id)
        {
            try
            {
                var item = await _service.GetBlog(id);
                Console.WriteLine($"Id => {item.BlogId}");
                Console.WriteLine($"Title => {item.BlogTitle}");
                Console.WriteLine($"Author => {item.BlogAuthor}");
                Console.WriteLine($"Content => {item.BlogContent}");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
        }

        private async Task DeleteAsync(int id)
        {
            var message = await _service.DeleteBlog(id);
            Console.WriteLine(message);
        }

        private async Task CreateAsync(string title, string author, string content)
        {
            BlogModel model = new BlogModel()
            {
                BlogAuthor = author,
                BlogTitle = title,
                BlogContent = content
            };
            var message= await _service.CreateBlog(model);
            Console.WriteLine(message);
        }  
        private async Task UpdateAsync(int id,string title, string author, string content)
        {
            BlogModel model = new BlogModel()
            {
                BlogAuthor = author,
                BlogTitle = title,
                BlogContent = content
            };
            var message= await _service.UpdateBlog(id, model);
            Console.WriteLine(message);
        }
    }
}
