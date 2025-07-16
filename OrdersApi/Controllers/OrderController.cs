using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersApi.Data;
using OrdersApi.Models;
using OrdersApi.Services;

namespace OrdersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly AppDbContext context;
        private readonly MessageBusService serviceBus;

        public OrderController(AppDbContext context, MessageBusService bus)
        {
            this.context = context;
            this.serviceBus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto body)
        {
            var entry = this.context.Orders.Add(new Order
            {
                ProductName = body.ProductName,
                Quantity = body.Quantity,
                Status = "Pending"
            });
            await this.context.SaveChangesAsync();

            var order = entry.Entity;

            await this.serviceBus.SendMessageAsync(order);

            return CreatedAtAction(nameof(CreateOrder), new { id = order.Id }, body);
        }
    }
}
