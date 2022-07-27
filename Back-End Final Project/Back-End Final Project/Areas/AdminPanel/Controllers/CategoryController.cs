using Back_End_Final_Project.DAL;
using Back_End_Final_Project.Extensions;
using Back_End_Final_Project.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IWebHostEnvironment _env;
        public CategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid) return View();            
            if(category.Picture == null)
            {
                ModelState.AddModelError("Picture", "Please choose picture");
                return View();
            }
            if (!category.Picture.ImageIsOkey(2))
            {
                ModelState.AddModelError("Picture", "Please coho0se correct picture");
                return View();
            }
            category.Image = await category.Picture.FileCreate(_env.WebRootPath, "assets/img");
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Edit(int? id, Category NewCategory)
        {
            if (id == null || id == 0) return NotFound();
            Category existed = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (!ModelState.IsValid) return View(existed);
            if (existed == null) return NotFound();
            bool RepeatCategory = _context.Categories.Any(c =>c.Id!=existed.Id&& c.Name == NewCategory.Name);
            if (RepeatCategory)
            {
                ModelState.AddModelError("Name", "can not dubilcate name");
                return View();
            }    
            _context.Entry(existed).CurrentValues.SetValues(NewCategory);
            if (NewCategory.Picture!=null)
            {
                if (!NewCategory.Picture.ImageIsOkey(2))
                {
                    ModelState.AddModelError("Image", "choose valid image");
                    return View();
                }
                if(existed.Image!=null)
                    FileExtension.FileDelete(_env.WebRootPath, "assets/img", existed.Image);

                existed.Image = await FileExtension.FileCreate(NewCategory.Picture, _env.WebRootPath, "assets/img");
            }
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

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }
    }
}
