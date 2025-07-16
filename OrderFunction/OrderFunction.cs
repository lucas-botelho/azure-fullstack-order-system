using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using OrderFunction.Data;
using OrderFunction.Models;

namespace OrderFunction
{
    public class OrderFunction
    {
        [FunctionName("ProcessOrder")]
        public async Task Run(
            [ServiceBusTrigger("orders", Connection = "ServiceBusConnection")] string myQueueItem,
            ILogger log)
        {
            try
            {
                var connectionString = Environment.GetEnvironmentVariable("SqlConnection");

                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(connectionString);

                using var dbContext = new AppDbContext(optionsBuilder.Options);

                var order = JsonSerializer.Deserialize<Order>(myQueueItem);
                if (order == null)
                {
                    log.LogWarning("Invalid message from service bus.");
                    return;
                }

                var existing = await dbContext.Orders.FindAsync(order.Id);
                if (existing != null)
                {
                    existing.Status = "Processed";
                    await dbContext.SaveChangesAsync();
                    log.LogInformation($"Order {existing.Id} changed to 'Processed'.");
                }
                else
                {
                    log.LogWarning($"Order with ID {order.Id} not found.");
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Failed to process message: " + myQueueItem + " : " + ex.Message);
                throw;
            }
        }
    }

}
