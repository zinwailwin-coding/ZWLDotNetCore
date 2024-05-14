using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZWLDotNetCore.PizzaApi.Db;

internal class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
    }
    public DbSet<PizzaModel> Pizzas { get; set; }
    public DbSet<ExtraPizzaModel> ExtraPizzas { get; set; }
    public DbSet<OrderModel> Orders { get; set; }
    public DbSet<OrderDetailModel> OrderDetails { get; set; }
}

[Table("Tbl_Pizza")]
public class PizzaModel
{
    [Key]
    public int PizzaId { get; set; }
    public string PizzaName { get; set; }
    public decimal Price { get; set; }

}


[Table("Tbl_ExtraPizza")]
public class ExtraPizzaModel
{
    [Key]
    public int ExtraPizzaId { get; set; }
    public string ExtraPizzaName { get; set; }
    public decimal Price { get; set; }

    [NotMapped]
    public string PriceStr { get { return "$ " + Price; } }

}

public class OrderRequest
{
    public int PizzaId { get; set; }
    public int[] ExtraId { get; set; }
}

public class OrderResponse
{
    public string Message { get; set; }
    public string InvoiceNo { get; set; }
    public decimal TotalAmount { get; set; }
}


[Table("Tbl_Order")]
public class OrderModel
{
    [Key]
    public int OrderId { get; set; }
    public string InvoiceNo { get; set; }
    public int PizzaId { get; set; }
    public decimal TotalPrice { get; set; }

}

[Table("Tbl_OrderDetail")]
public class OrderDetailModel
{
    [Key]
    public int OrderDetailId { get; set; }
    public string InvoiceNo { get; set; }
    public int ExtraPizzaId { get; set; }
}


