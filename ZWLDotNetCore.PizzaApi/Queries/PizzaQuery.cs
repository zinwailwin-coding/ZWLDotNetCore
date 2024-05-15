namespace ZWLDotNetCore.PizzaApi.Queries
{
    public class PizzaQuery
    {
        public static string orderQuery { get; } = 
            @"select o.*,PizzaName,Price from Tbl_Order o
            inner join Tbl_Pizza p
            on o.PizzaId=  p.PizzaId
            where o.InvoiceNo=@InvoiceNo
            ";

        public static string orderDetailQuery { get; } = 
            @"select od.*,ExtraPizzaName,Price from Tbl_OrderDetail od
            inner join Tbl_ExtraPizza ep
            on od.ExtraPizzaId= ep.ExtraPizzaId
            where od.InvoiceNo=@InvoiceNo
            ";
    }
}
