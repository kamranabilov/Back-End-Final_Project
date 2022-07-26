using Back_End_Final_Project.DAL;
using Back_End_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End_Final_Project.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> model = _context.Categories.OrderByDescending(m => m.Id).ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid) return View();
            Category existed = _context.Categories
                .FirstOrDefault(c => c.Name.ToLower().Trim() == category.Name.ToLower().Trim());
            if (existed != null)
            {
                ModelState.AddModelError("Name", "Category name must be unique");
                return View();
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int? id, Category NewCategory)
        {
            if (id == null || id == 0) return NotFound();
            if (!ModelState.IsValid) return View();
            Category existed = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (existed == null) return NotFound();
            bool RepeatCategory = _context.Categories.Any(c => c.Name == NewCategory.Name);
            if (RepeatCategory)
            {
                ModelState.AddModelError("Name", "Name must be unique");
                return View();
            }

            _context.Entry(existed).CurrentValues.SetValues(NewCategory);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Category category =await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
