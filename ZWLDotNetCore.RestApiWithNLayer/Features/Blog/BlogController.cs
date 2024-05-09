using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ZWLDotNetCore.RestApiWithNLayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BL_Blog _blBlog;
        public BlogController()
        {
            _blBlog = new BL_Blog();
        }

        [HttpGet]
        public IActionResult Read()
        {
            var list = _blBlog.GetBlogs();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var blog = _blBlog.GetBlog(id);
            if (blog is null)
            {
                return NotFound("No Data Found");
            }
            return Ok(blog);
        }
        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {           
            var result = _blBlog.CreateBlog(blog);
            string message = result > 0 ? "Create Successful" : "Create Failed";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var _blog = _blBlog.GetBlog(id);
            if (_blog is null)
            {
                return NotFound("No Data Found");
            }
           var result = _blBlog.UpdateBlog(id, blog);
            string message = result > 0 ? "Update Successful" : "Update Failed";
            return Ok(message);
        }
        //[HttpPatch("{id}")]
        //public IActionResult Patch(int id, BlogModel blog)
        //{
        //    var _blog = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
        //    if (_blog is null)
        //    {
        //        return NotFound("No Data Found");
        //    }
        //    if (!string.IsNullOrEmpty(blog.BlogTitle)) _blog.BlogTitle = blog.BlogTitle;
        //    if (!string.IsNullOrEmpty(blog.BlogAuthor)) _blog.BlogAuthor = blog.BlogAuthor;
        //    if (!string.IsNullOrEmpty(blog.BlogContent)) _blog.BlogContent = blog.BlogContent;
        //    var result = _context.SaveChanges();
        //    string message = result > 0 ? "Update Successful" : "Update Failed";
        //    return Ok(message);
        //}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var blog = _blBlog.GetBlog(id);
            if (blog is null)
            {
                return NotFound("No Data Found");
            }
            var result = _blBlog.DeleteBlog(id);
            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            return Ok(message);
        }
    }
}
