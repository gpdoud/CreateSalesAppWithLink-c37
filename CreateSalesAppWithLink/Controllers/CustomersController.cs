using CreateSalesAppWithLink.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLink.Controllers {
    public class CustomersController {

        private readonly AppDbContext _context;
        public CustomersController(AppDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAll() {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByPK(int id) {
            return await _context.Customers.FindAsync(id);
        }

        public async Task Update(int id, Customer customer) {
            if(id != customer.Id) {
                throw new ArgumentException("Ids don't match!");
            }
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> Insert(Customer customer) {
            if(customer.Id != 0) {
                throw new ArgumentException("Id must be zero!");
            }
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task Delete(int id) {
            var item = await GetByPK(id);
            if(item is null) {
                throw new Exception("Not found!");
            }
            _context.Customers.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
