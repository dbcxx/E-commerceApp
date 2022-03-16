using Microsoft.AspNetCore.Mvc;
using E_commerceApp.Data;
using System.Linq;
using System.Threading.Tasks;
using E_commerceApp.Models;

namespace E_commerceApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTagsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductTagsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //var data = _db.productTags.ToList();
            return View(_db.ProductTags.ToList());
        }

        //get action method

        public ActionResult Create()
        {
            return View();
        }

        //post action method 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTags productTags)
        {
            if (ModelState.IsValid)
            {
                _db.ProductTags.Add(productTags);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTags);
        }

        //get action method

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productTag = _db.ProductTags.FirstOrDefault(x => x.Id == id);
            if (productTag == null)
            {
                return NotFound();
            }
            return View(productTag);
        }

        //post action method 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTags productTags)
        {
            if (ModelState.IsValid)
            {
                _db.Update(productTags);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTags);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productTag = _db.ProductTags.FirstOrDefault(x => x.Id == id);
            if (productTag == null)
            {
                return NotFound();
            }
            return View(productTag);
        }

        //post action method 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTags productTags)
        {

            return RedirectToAction(nameof(Index));


        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productTag = _db.ProductTags.Find(id);
            if (productTag == null)
            {
                return NotFound();
            }
            return View(productTag);
        }

        //post action method 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, ProductTags productTags)
        {
            var productTag = _db.ProductTags.Find(id);
            if (id == null || id != productTags.Id || productTag == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Remove(productTag);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTags);
        }
    }
}
