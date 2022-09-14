using CreateSalesAppWithLink.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLink.Controllers {
    public class ProductsController {

        private readonly AppDbContext _context;
        public ProductsController(AppDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAll() {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByPK(int id) {
            return await _context.Products.FindAsync(id);
        }

        public async Task Update(int id, Product product) {
            if (id != product.Id) {
                throw new ArgumentException("Ids don't match!");
            }
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Product> Insert(Product product) {
            if (product.Id != 0) {
                throw new ArgumentException("Id must be zero!");
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task Delete(int id) {
            var item = await GetByPK(id);
            if (item is null) {
                throw new Exception("Not found!");
            }
            _context.Products.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
