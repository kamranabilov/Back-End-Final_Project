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
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
           _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Slider> model = _context.Sliders.ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if(!ModelState.IsValid) return View();
            if (!slider.Picture.ImageIsOkey(3))
            {
                ModelState.AddModelError("Picture", "choose image file");
                return View();
            }

            slider.Image = await slider.Picture.FileCreate(_env.WebRootPath, "assets/img");

            await _context.Sliders.AddAsync(slider);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null) return NotFound();
            return View(slider);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, Slider slider)
        {
            if (id == null || id == 0) return NotFound();
            Slider existed = await _context.Sliders.FindAsync(id);
            if (existed == null) return NotFound();
            if (!ModelState.IsValid) return View(slider);
            if (slider.Picture == null)
            {
                string filename = existed.Image;
                _context.Entry(existed).CurrentValues.SetValues(slider);
                existed.Image = filename;
            }
            else
            {
                if (!slider.Picture.ImageIsOkey(3))
                {
                    ModelState.AddModelError("Picture", "choose image file");
                    return View(existed);
                }
                FileExtension.FileDelete(_env.WebRootPath, "assets/img", existed.Image);
                _context.Entry(existed).CurrentValues.SetValues(slider);
                existed.Image = await slider.Picture.FileCreate(_env.WebRootPath, "assets/img");
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Slider slider = await _context.Sliders.FindAsync(id);
            if (slider == null) return NotFound();
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
