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
        public OrderlinesController(AppDbContext context) {
            _context = context;
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
        }

        public async Task<Orderline> Insert(Orderline orderline) {
            if (orderline.Id != 0) {
                throw new ArgumentException("Id must be zero!");
            }
            _context.Orderlines.Add(orderline);
            await _context.SaveChangesAsync();
            return orderline;
        }

        public async Task Delete(int id) {
            var item = await GetByPK(id);
            if (item is null) {
                throw new Exception("Not found!");
            }
            _context.Orderlines.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
