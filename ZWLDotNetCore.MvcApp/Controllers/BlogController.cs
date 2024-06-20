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
            var lst= await _db.Blogs.ToListAsync();
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
    }
}
