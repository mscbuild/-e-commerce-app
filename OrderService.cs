 using EcommerceApp.Data;
using EcommerceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Services
{
    public class OrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string Message)> CreateOrderAsync(int customerId, List<(int ProductId, int Quantity)> items)
        {
            // Validate customer
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
                return (false, "Customer not found.");

            // Validate products and stock
            var productIds = items.Select(i => i.ProductId).ToList();
            var products = await _context.Products
                                         .Where(p => productIds.Contains(p.Id))
                                         .ToDictionaryAsync(p => p.Id);

            foreach (var item in items)
            {
                if (!products.ContainsKey(item.ProductId))
                    return (false, $"Product ID {item.ProductId} not found.");

                if (products[item.ProductId].Stock < item.Quantity)
                    return (false, $"Insufficient stock for product {products[item.ProductId].Name}.");
            }

            // Create order
            var order = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.UtcNow,
                Status = "Pending",
                OrderItems = new List<OrderItem>(),
                TotalAmount = 0
            };

            foreach (var item in items)
            {
                var product = products[item.ProductId];

                order.OrderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });

                order.TotalAmount += product.Price * item.Quantity;
                product.Stock -= item.Quantity; // update inventory
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return (true, $"Order {order.Id} created successfully.");
        }
    }
}
