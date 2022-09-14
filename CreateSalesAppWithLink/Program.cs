using CreateSalesAppWithLink.Controllers;
using CreateSalesAppWithLink.Models;
using static System.Diagnostics.Debug;

WriteLine("Create Sales App");

AppDbContext _context = new();
CustomersController custCtrl = new(_context);
OrdersController ordCtrl = new(_context);
ProductsController prodCtrl = new(_context);
OrderlinesController ordlCtrl = new(_context);

//Orderline ordl = new Orderline {
//    Id = 0, OrderId = 9, ProductId = 9, Quantity = 1
//};
//await ordlCtrl.Insert(ordl);

var orderline = await ordlCtrl.GetByPK(9);
/*

orderline.Quantity = 2;

if(orderline is not null) 
    await ordlCtrl.Update(orderline.Id, orderline);
*/
if(orderline is not null) 
    await ordlCtrl.Delete(orderline.Id);