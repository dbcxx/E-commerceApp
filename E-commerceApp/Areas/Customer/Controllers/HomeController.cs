using E_commerceApp.Data;
using E_commerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerceApp.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
           // _logger = logger;
           _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Products.Include(c=>c.ProductTypes).Include(c=>c.ProductTags).ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //get
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var product =_db.Products.Include(c=>c.ProductTypes).Include(c=>c.ProductTags).FirstOrDefault(c=>c.ID==id);
           if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
