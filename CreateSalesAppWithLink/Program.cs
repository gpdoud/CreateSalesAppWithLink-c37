using CreateSalesAppWithLink.Controllers;
using CreateSalesAppWithLink.Models;

Console.WriteLine("Create Sales App");

AppDbContext _context = new();
CustomersController custCtrl = new(_context);
OrdersController ordCtrl = new(_context);
ProductsController prodCtrl = new(_context);
OrderlinesController ordlCtrl = new(_context);

