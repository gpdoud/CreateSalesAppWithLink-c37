using CreateSalesAppWithLink.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLink.Controllers {
    public class OrdersController {

        private readonly AppDbContext _context;
        public OrdersController(AppDbContext context) {
            _context = context;
        }

        // Return the orders containing a product id
        public async Task<IEnumerable<Order>> GetOrderByProductId(int productId) {

            var orders = from o in _context.Orders
                         join l in _context.Orderlines on o.Id equals l.OrderId
                         join p in _context.Products on l.ProductId equals p.Id
                         where p.Id == productId
                         select o;

            return await orders.ToListAsync();
        }

        // Return a list of orders for a customer by code
        public async Task<IEnumerable<Order>> GetOrdersByCustomerCode(string code) {
            //var orders = from o in _context.Orders
            //             join c in _context.Customers
            //                 on o.CustomerId equals c.Id
            //             where c.Code == code
            //             select o;

            //return await orders.ToListAsync();

            var orders = _context.Orders
                .Join(_context.Customers, o => o.CustomerId, c => c.Id, (Order, c) => new { Order })
                .Select(o => o.Order);

            return await orders.ToListAsync();
        }

        // Return a list of orders for a customer
        public async Task<IEnumerable<Order>> GetOrdersByCustomer(int customerId) {
            //return await _context.Orders.Where(o => o.CustomerId == customerId).ToListAsync();

            var orders = from o in _context.Orders
                         where o.CustomerId == customerId
                         select o;
            return await orders.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAll() {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order?> GetByPK(int id) {
            return await _context.Orders.FindAsync(id);
        }

        public async Task Update(int id, Order order) {
            if (id != order.Id) {
                throw new ArgumentException("Ids don't match!");
            }
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task SetStatusToInprocessForNonZeroTotal(int id, Order order) {
            // if the total is still zero, skip updating it.
            if(order.Total == 0) {
                return;
            }
            order.Status = "INPROCESS";
            await Update(id, order);
        }

        public async Task SetStatusToClosed(int id, Order order) {
            order.Status = "CLOSED";
            await Update(id, order);
        }

        public async Task<Order> Insert(Order order) {
            if (order.Id != 0) {
                throw new ArgumentException("Id must be zero!");
            }
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task Delete(int id) {
            var item = await GetByPK(id);
            if (item is null) {
                throw new Exception("Not found!");
            }
            _context.Orders.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
