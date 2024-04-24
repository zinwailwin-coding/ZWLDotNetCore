using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZWLDotNetCore.ConsoleApp
{
    internal class EFCoreExample
    {
        private readonly AppDbContext db = new AppDbContext();
        public void Run()
        {
            //Read();
            // Edit(2002);
            //Create("Harry Potter", "JK Rolling", "Horror");
            //Update(2003, "Harry Potter", "JK Rolling", "Supernatural");
            Delete(2003);
        }
        private void Read()
        {
            var blogList = db.Blogs.ToList();
            foreach (BlogDto blog in blogList)
            {
                Console.WriteLine(blog.BlogId);
                Console.WriteLine(blog.BlogTitle);
                Console.WriteLine(blog.BlogAuthor);
                Console.WriteLine(blog.BlogContent);
                Console.WriteLine("---------------------------");
            }
        }

        private void Edit(int id)
        {
            var blog = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (blog is null)
            {
                Console.WriteLine("Data not found");
                return;
            }
            Console.WriteLine(blog.BlogId);
            Console.WriteLine(blog.BlogTitle);
            Console.WriteLine(blog.BlogAuthor);
            Console.WriteLine(blog.BlogContent);
        }

        private void Create(string title, string author, string content)
        {
            var blog = new BlogDto()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            db.Blogs.Add(blog);
            int result = db.SaveChanges();
            string message = result > 0 ? "Create Successful" : "Create Failed";
            Console.WriteLine(message);
        }

        private void Update(int id, string title, string author, string content)
        {
            var blog = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (blog is null)
            {
                Console.WriteLine("Data not found");
                return;
            }
            blog.BlogTitle = title;
            blog.BlogAuthor = author;
            blog.BlogContent = content;
            var result = db.SaveChanges();
            string message = result > 0 ? "Update Successful" : "Update Failed";
            Console.WriteLine(message);
        }
        private void Delete(int id)
        {
            var blog = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (blog is null)
            {
                Console.WriteLine("Data not found");
                return;
            }
            db.Blogs.Remove(blog);
            var result = db.SaveChanges();
            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            Console.WriteLine(message);
        }
    }

}

