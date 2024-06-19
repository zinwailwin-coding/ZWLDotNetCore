using Microsoft.EntityFrameworkCore;
using ZWLDotNetCore.MinimalApi.Db;
using ZWLDotNetCore.MinimalApi.Models;


namespace ZWLDotNetCore.MinimalApi.Features.Blogs
{
    public static class BlogService
    {
        public static IEndpointRouteBuilder AddBlogFeatures(this IEndpointRouteBuilder app)
        {
            app.MapGet(
                "api/Blog",
                async (AppDbContext db) =>
                {
                    var lst = await db.Blogs.AsNoTracking().ToListAsync();
                    return Results.Ok(lst);
                }
            );

            app.MapPost(
                "api/Blog",
                async (AppDbContext db, BlogModel blog) =>
                {
                    await db.Blogs.AddAsync(blog);
                    var result = await db.SaveChangesAsync();
                    string message = result > 0 ? "Create Successful" : "Create Failed";
                    return Results.Ok(message);
                }
            );

            app.MapPut(
                "api/Blog/{id}",
                async (AppDbContext db, int id, BlogModel blog) =>
                {
                    var _blog = await db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
                    if (_blog is null)
                    {
                        return Results.NotFound("No Data Found");
                    }
                    _blog.BlogTitle = blog.BlogTitle;
                    _blog.BlogAuthor = blog.BlogAuthor;
                    _blog.BlogContent = blog.BlogContent;
                    var result = await db.SaveChangesAsync();
                    string message = result > 0 ? "Update Successful" : "Update Failed";
                    return Results.Ok(message);
                }
            );

            app.MapDelete(
                "api/Blog/{id}",
                async (AppDbContext db, int id) =>
                {
                    var _blog = await db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
                    if (_blog is null)
                    {
                        return Results.NotFound("No Data Found");
                    }
                    db.Blogs.Remove(_blog);
                    var result = await db.SaveChangesAsync();
                    string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
                    return Results.Ok(message);
                }
            );
            return app;
        }
    }
}
