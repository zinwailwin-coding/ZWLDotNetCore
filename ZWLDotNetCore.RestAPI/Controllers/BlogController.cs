using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZWLDotNetCore.RestAPI.Db;
using ZWLDotNetCore.RestAPI.Models;

namespace ZWLDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        //private readonly AppDbContext _context;
        //public BlogController()
        //{
        //    _context= new AppDbContext();
        //}

        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Read()
        {
            var list= _context.Blogs.ToList();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(x=>x.BlogId==id);
            if (blog is null)
            {
                return NotFound("No Data Found");
            }
            return Ok(blog);
        }
        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            _context.Blogs.Add(blog);
            var result= _context.SaveChanges();
            string message = result > 0 ? "Create Successful" : "Create Failed";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var _blog= _context.Blogs.FirstOrDefault(x=>x.BlogId==id);
            if (_blog is null)
            {
                return NotFound("No Data Found");
            }
            _blog.BlogTitle = blog.BlogTitle;
            _blog.BlogAuthor = blog.BlogAuthor;
            _blog.BlogContent = blog.BlogContent;
            var result = _context.SaveChanges();
            string message = result > 0 ? "Update Successful" : "Update Failed";
            return Ok(message);
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var _blog = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (_blog is null)
            {
                return NotFound("No Data Found");
            }
            if(!string.IsNullOrEmpty(blog.BlogTitle)) _blog.BlogTitle = blog.BlogTitle;
            if (!string.IsNullOrEmpty(blog.BlogAuthor)) _blog.BlogAuthor = blog.BlogAuthor;
            if (!string.IsNullOrEmpty(blog.BlogContent)) _blog.BlogContent = blog.BlogContent;
            var result = _context.SaveChanges();
            string message = result > 0 ? "Update Successful" : "Update Failed";
            return Ok(message);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var _blog = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (_blog is null)
            {
                return NotFound("No Data Found");
            }
            _context.Blogs.Remove(_blog);
            var result = _context.SaveChanges();
            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            return Ok(message);
        }
      
    }
}
