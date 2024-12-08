using Microsoft.AspNetCore.Mvc;
using CrudProject.Models;

using System.Linq;
namespace CrudProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject ApplicationDbContext
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index( ApplicationDbContext context)
        {
            var categories = _context.Categorys.ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                // Add the new category to the database
                _context.Categorys.Add(category);
                _context.SaveChanges();
                // Redirect to the Index action to view the list of categories
                return RedirectToAction(nameof(Index));
            }
            // If the model state is not valid, return the Create view with the current category data
            return View(category);
        }
    }
}
