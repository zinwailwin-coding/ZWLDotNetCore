using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using ZWLDotNetCore.RestAPI.Models;
using ZWLDotNetCore.Shared;

namespace ZWLDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNet2Controller : ControllerBase
    {
        private readonly AdoDotNetService _doDotNetService = new AdoDotNetService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from tbl_blog";            
            var lst = _doDotNetService.Query<BlogModel>(query);
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "select * from tbl_blog where @BlogId=BlogId";
            var result = _doDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameters("@BlogId", id));
            if(result is null)
            {
                return NotFound("No Data Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";           

            var result = _doDotNetService.Execute(query, 
                new AdoDotNetParameters("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameters("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameters("@BlogContent", blog.BlogContent));

            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id,BlogModel blog)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId=@BlogId";
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

            var result = _doDotNetService.Execute(query,
                new AdoDotNetParameters("@BlogId", id),
                new AdoDotNetParameters("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameters("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameters("@BlogContent", blog.BlogContent));

            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog) 
        {
            var item = findById(id);
            if(item is null)
            {
                return NotFound("No Data Found.");
            }
            string conditions = string.Empty;

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }
            else
            {
                blog.BlogTitle = item.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
            }
            else
            {
                blog.BlogAuthor = item.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += " [BlogContent] = @BlogContent, ";
            }
            else 
            { 
                blog.BlogContent = item.BlogContent;
            }

            if (conditions.Length == 0) { return NotFound("No Data Found"); }
            conditions = conditions.Substring(0, conditions.Length - 2);
            blog.BlogId = id;

            string query = $@"UPDATE [dbo].[Tbl_Blog]
               SET {conditions}
             WHERE BlogId=@BlogId";

            var result = _doDotNetService.Execute(query,
                new AdoDotNetParameters("@BlogId",id),
                 new AdoDotNetParameters("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameters("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameters("@BlogContent", blog.BlogContent));

            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"delete from Tbl_Blog where BlogId=@BlogId";    
            var result = _doDotNetService.Execute(query, new AdoDotNetParameters("@BlogId", id));

            string message = result > 0 ? "Deleted Successful" : "Deleted Failed";
            return Ok(message);
        }

        private BlogModel findById(int id)
        {
            string query = "select * from tbl_blog where BlogId=@BlogId";
            var blog = _doDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameters("@BlogId", id));
            return blog;
        }
    }
}
