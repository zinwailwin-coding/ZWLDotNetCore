using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZWLDotNetCore.PizzaApi.Db;

namespace ZWLDotNetCore.PizzaApi.Features.Pizza
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PizzaController()
        {
            _context = new AppDbContext();
        }

        [HttpGet]
        public async Task<IActionResult> GetPizzaList()
        {
            var lst=await _context.Pizzas.ToListAsync();
            return Ok(lst);
        }

        [HttpGet("GetPizzaExtraList")]
        public async Task<IActionResult> GetPizzaExtraList()
        {
            var lst = await _context.ExtraPizzas.ToListAsync();
            return Ok(lst);
        }

        [HttpPost("OrderRequest")]
        public async Task<IActionResult> OrderAsync(OrderRequest orderRequest)
        {
            var pizza = await _context.Pizzas.FirstOrDefaultAsync(x=>x.PizzaId== orderRequest.PizzaId);
            var price = pizza.Price;
            var invoiceNo = DateTime.Now.ToString("yyyyMMddHHmmss");

            var extraPizza = await _context.ExtraPizzas.Where(x=>orderRequest.ExtraId.Contains(x.ExtraPizzaId)).ToListAsync();
            price += extraPizza.Sum(x => x.Price);
            OrderModel model = new OrderModel()
            {
                PizzaId = orderRequest.PizzaId,
                InvoiceNo = invoiceNo,
                TotalPrice= price
            };

            List<OrderDetailModel> orderDetailModels = orderRequest.ExtraId.Select(id => new OrderDetailModel()
            {
                ExtraPizzaId = id,
                InvoiceNo = invoiceNo,
            }).ToList();
            await _context.Orders.AddAsync(model);
            await _context.OrderDetails.AddRangeAsync(orderDetailModels);
            await _context.SaveChangesAsync();

            OrderResponse orderResponse = new OrderResponse()
            {
                InvoiceNo = invoiceNo,
                TotalAmount = price,
                Message = "Thank you for your order! Enjoy your pizza!",
            };
            return Ok(orderResponse);
        }


        [HttpGet("Order/{invoiceNo}")]
        public async Task<IActionResult> GetOrderDetail(string invoiceNo)
        {
            var order= await _context.Orders.FirstOrDefaultAsync(x=>x.InvoiceNo == invoiceNo);
            var orderDetail = await _context.OrderDetails.Where(x=>x.InvoiceNo==invoiceNo).ToListAsync();
            return Ok(new
            {
                Order = order,
                OrderDetail = orderDetail
            });
        }
    }
}
