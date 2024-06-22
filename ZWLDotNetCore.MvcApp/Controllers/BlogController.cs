using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZWLDotNetCore.MvcApp.Db;
using ZWLDotNetCore.MvcApp.Models;

namespace ZWLDotNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;

        public BlogController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var lst = await _db.Blogs.OrderByDescending(x => x.BlogId).ToListAsync();
            return View(lst);
        }

        [ActionName("Create")]
        public IActionResult CreateForm()
        {
            return View("CreateForm");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> SaveCreate(BlogModel blog)
        {
            await _db.Blogs.AddAsync(blog);
            var result = await _db.SaveChangesAsync();
            return Redirect("/Blog");
        }

        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> EditBlog(int id)
        {
            var blog = await _db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
            if (blog is null)
            {
                return Redirect("/Blog");
            }
            return View("EditBlog", blog);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> UpdateBlog(int id, BlogModel blog)
        {
            var item = await _db.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null)
            {
                return Redirect("/Blog");
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            _db.Entry(item).State = EntityState.Modified;

            await _db.SaveChangesAsync();
            return Redirect("/Blog");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var blog = await _db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
            if (blog is null)
            {
                return Redirect("/Blog");
            }
            _db.Blogs.Remove(blog);
            await _db.SaveChangesAsync();
            return Redirect("/Blog");
        }
    }
}
