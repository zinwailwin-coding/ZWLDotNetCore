using Microsoft.EntityFrameworkCore;
using ZWLDotNetCore.MVCApp2.Models;

namespace ZWLDotNetCore.MVCApp2.Db;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
    //}
    public DbSet<BlogModel> Blogs { get; set; }
}
