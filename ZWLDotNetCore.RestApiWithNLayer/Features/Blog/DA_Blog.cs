using Microsoft.AspNetCore.Http.HttpResults;
using ZWLDotNetCore.RestApiWithNLayer.Db;

namespace ZWLDotNetCore.RestApiWithNLayer.Features.Blog
{
    public class DA_Blog
    {
        private readonly AppDbContext _context;

        public DA_Blog()
        {
            _context = new AppDbContext();
        }

        public List<BlogModel> GetBlogs() {
            var lst=_context.Blogs.ToList();
            return lst;
        }
        public BlogModel GetBlog(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(x=>x.BlogId==id);
            return blog!;
        }

        public int CreateBlog(BlogModel requestModel)
        {
            _context.Blogs.Add(requestModel);
            var result= _context.SaveChanges();
            return result;
        }

        public int UpdateBlog(int id,BlogModel requestModel)
        {
            var blog = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (blog is null)
            {
                return 0;
            }

            blog.BlogTitle = requestModel.BlogTitle;
            blog.BlogAuthor = requestModel.BlogAuthor;
            blog.BlogContent = requestModel.BlogContent;
          
            var result = _context.SaveChanges();
            return result;
        }

        public int DeleteBlog(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(blog is null) return 0;
            _context.Blogs.Remove(blog);
            var result = _context.SaveChanges();
            return result;
        }
    }
}
