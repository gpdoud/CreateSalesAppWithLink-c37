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
