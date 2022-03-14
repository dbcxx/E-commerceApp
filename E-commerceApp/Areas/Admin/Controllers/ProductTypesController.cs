using Microsoft.AspNetCore.Mvc;
using E_commerceApp.Data;
using System.Linq;
using System.Threading.Tasks;
using E_commerceApp.Models;

namespace E_commerceApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductTypesController(ApplicationDbContext db)
        {
            _db=db;
        }
        public IActionResult Index()
        {
            //var data = _db.productTypes.ToList();
            return View(_db.ProductTypes.ToList()) ;
        }

        //get action method

        public ActionResult Create()
        {
            return View();
        }

        //post action method 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.ProductTypes.Add(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }

        //get action method

        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.FirstOrDefault(x => x.Id == id);
            if(productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        //post action method 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.Update(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.FirstOrDefault(x => x.Id == id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        //post action method 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTypes productTypes)
        {
            
                return RedirectToAction(nameof(Index));
           
            
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        //post action method 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete( int? id , ProductTypes productTypes)
        {
            var productType = _db.ProductTypes.Find(id);
            if(id == null || id != productTypes.Id || productType == null )
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                _db.Remove(productType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }
    }
}
