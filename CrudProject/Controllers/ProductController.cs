using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using CrudProject.Models;
using System.Linq;

namespace CrudProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

      
        public IActionResult Index()
        {
            
            var products = _context.Products.Include(p => p.Category).ToList();
            return View(products);
        }

       
        [HttpGet]
        public IActionResult Create()
        {
            // Fetch categories for the dropdown
            ViewBag.Categories = _context.Categorys.ToList();
            return View();
        }


        // POST Create Action - Handle form submission for creating product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                // Add the new product to the database
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // In case of error, fetch categories and return view with validation errors
            ViewBag.Categories = _context.Categorys.ToList();
            return View(product);
        }

        
        [HttpGet]
        public IActionResult Edit(int id)
        {
           
            var product = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = _context.Categorys.ToList();
            return View(product);
        }

        
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.Update(product);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Products.Any(p => p.Id == product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // If validation failed, fetch categories and redisplay form
            ViewBag.Categories = _context.Categorys.ToList();
            return View(product);
        }

        // GET Delete Action - Show confirmation page for deleting product
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST Delete Action - Handle product deletion
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
