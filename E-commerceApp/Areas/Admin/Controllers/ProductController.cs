using E_commerceApp.Data;
using E_commerceApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerceApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext _db;
        private IWebHostEnvironment _host;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment host)
        {
            _db = db;
            _host = host;
        }
        public IActionResult Index()
        {
            return View(_db.Products.Include(a=>a.ProductTypes).Include(a=>a.ProductTags).ToList());
        }

        //post
        [HttpPost]
        public  IActionResult Index(decimal? lowAmount, decimal? largeAmount)
        {
            var products = _db.Products.Include(c => c.ProductTypes).Include(c => c.ProductTags)
                .Where(c => c.Price >= lowAmount && c.Price <= largeAmount).ToList();
            if(lowAmount==null || largeAmount == null)
            {
                products = _db.Products.Include(c => c.ProductTypes).Include(c => c.ProductTags).ToList();
            }

            return View(products);
        }

        //get
        public IActionResult Create()
        {
            ViewData["ProductTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(_db.ProductTags.ToList(), "Id", "ProductTag");
            return View();
        }

        //post 
        [HttpPost]
        
        public async Task<IActionResult> Create(Products product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var searchProduct = _db.Products.FirstOrDefault(c=>c.Name == product.Name);
                if (searchProduct!= null)
                {

                    ViewData["ProductTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
                    ViewData["TagId"] = new SelectList(_db.ProductTags.ToList(), "Id", "ProductTag");
                    return View(product);
                }
                if(image!= null)
                {
                    var name = Path.Combine(_host.WebRootPath + "/images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "images/" + image.FileName;
                }
                if (image == null)
                {
                    product.Image = "images/noimage.PNG";
                }
                _db.Products.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
             return View(product);
        }

        //get
        public IActionResult Edit(int? id)
        {
            ViewData["ProductTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(_db.ProductTags.ToList(), "Id", "ProductTag");
            

            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products
                .Include(b => b.ProductTypes)
                .Include(b => b.ProductTags)
                .FirstOrDefault(b => b.ID == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //post
        [HttpPost]
        public async Task<IActionResult> Edit(Products products, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_host.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    products.Image = "Images/noimage.PNG";
                }
                _db.Products.Update(products);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        //get
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products
                .Include(b => b.ProductTypes)
                .Include(b=>b.ProductTags)
                .FirstOrDefault(b=>b.ID==id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //get 
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products
                .Include(b=>b.ProductTypes)
                .Include(b=>b.ProductTags)
                .Where(b=>b.ID==id)
                .FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //post action method 
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.FirstOrDefault(c => c.ID == id);
            if(product == null)
            {
                return NotFound();
            }
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
