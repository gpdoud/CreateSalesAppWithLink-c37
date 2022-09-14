using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLink.Models {
    
    public class AppDbContext : DbContext {

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Orderline> Orderlines { get; set; }

        // Program.cs: AppDbContext adc1 = new();
        // Program.cs: OrdersController ordCtrl = new(adc1);
        // AppDbContext adc2 = new AppDbContext(dbContextoptions);

        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var connStr = @"server=localhost\sqlexpress;" +
                           "database=SalesDb37;" +
                           "trusted_connection=true;";
            if(!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(connStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
