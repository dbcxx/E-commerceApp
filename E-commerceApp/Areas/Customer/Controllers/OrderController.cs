using E_commerceApp.Data;
using E_commerceApp.Models;
using E_commerceApp.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerceApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        //get
        public IActionResult Checkout()
        {
            return View();
        }

        //post 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Order order)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if(products != null)
            {
                foreach(var product in products)
                {
                    OrderDetails orderDetails = new OrderDetails();
                    orderDetails.PorductId = product.ID;
                    order.OrderDetails.Add(orderDetails);
                }
            }

            order.OrderNo = GetOrderNo();
            _db.Order.Add(order);
            await _db.SaveChangesAsync();
            HttpContext.Session.Set("products", new List<Products>());
            return View();
        }

        private string GetOrderNo()
        {
            int numbersOfOrder = _db.Order.ToList().Count();
            return numbersOfOrder.ToString("000");
        }
    }
}
