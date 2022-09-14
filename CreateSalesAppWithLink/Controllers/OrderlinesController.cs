using CreateSalesAppWithLink.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLink.Controllers {
    public class OrderlinesController {

        private readonly AppDbContext _context;
        private OrdersController ordCtrl;
        public OrderlinesController(AppDbContext context) {
            _context = context;
            ordCtrl = new(_context);
        }

        private async Task RecalculateOrderTotal(int orderId) {
            var order = await ordCtrl.GetByPK(orderId);
            if(order is null) {
                throw new Exception("Order not found!");
            }
            order.Total = (from l in _context.Orderlines
                           join p in _context.Products on l.ProductId equals p.Id
                           where l.OrderId == orderId
                           select new {
                               LineTotal = l.Quantity * p.Price
                           }).Sum(x => x.LineTotal);

            await ordCtrl.Update(order.Id, order);
        }

        public async Task<IEnumerable<Orderline>> GetAll() {
            return await _context.Orderlines.ToListAsync();
        }

        public async Task<Orderline?> GetByPK(int id) {
            return await _context.Orderlines.FindAsync(id);
        }

        public async Task Update(int id, Orderline orderline) {
            if (id != orderline.Id) {
                throw new ArgumentException("Ids don't match!");
            }
            _context.Entry(orderline).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            await RecalculateOrderTotal(orderline.OrderId);
        }

        public async Task<Orderline> Insert(Orderline orderline) {
            if (orderline.Id != 0) {
                throw new ArgumentException("Id must be zero!");
            }
            _context.Orderlines.Add(orderline);
            await _context.SaveChangesAsync();
            await RecalculateOrderTotal(orderline.OrderId);
            return orderline;
        }

        public async Task Delete(int id) {
            var orderline = await GetByPK(id);
            if (orderline is null) {
                throw new Exception("Not found!");
            }
            _context.Orderlines.Remove(orderline);
            await _context.SaveChangesAsync();
            await RecalculateOrderTotal(orderline.OrderId);
        }
    }
}
