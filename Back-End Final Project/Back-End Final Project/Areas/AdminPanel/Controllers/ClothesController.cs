using Back_End_Final_Project.DAL;
using Back_End_Final_Project.Extensions;
using Back_End_Final_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End_Final_Project.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    //[Authorize(Roles = "Admin, Superadmin")]
    public class ClothesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ClothesController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Clothes> model = _context.Clothes
                .Include(c => c.ClothesInformation)
                .Include(c => c.Categories)
                .Include(c => c.ClothesImages).ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Information = _context.ClothesInformations.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Clothes clothes)
        {
                ViewBag.Information = _context.ClothesInformations.ToList();
                ViewBag.Categories = _context.Categories.ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (clothes.MainPhoto == null || clothes.Photos == null)
            {
               
                ModelState.AddModelError(string.Empty, "must choose 1 main photo");
                return View();
            }
            if (!clothes.MainPhoto.ImageIsOkey(2))
            {

                ModelState.AddModelError(string.Empty, "choose image file");
                return View();
            }

            clothes.ClothesImages = new List<ClothesImage>();
            TempData["Filename"] = "";
            List<IFormFile> removeable = new List<IFormFile>();
            foreach (var photo in clothes.Photos.ToList())
            {
                if (!photo.ImageIsOkey(2))
                {
                    removeable.Add(photo);
                    TempData["Filename"] += photo.FileName + ",";
                    continue;
                }
                ClothesImage otherphoto = new ClothesImage
                {
                    Name = await photo.FileCreate(_env.WebRootPath, "assets/img"),
                    IsMain = false,
                    Alternative = clothes.Name,
                    Clothes = clothes
                };
                clothes.ClothesImages.Add(otherphoto);
            }
            clothes.ClothesImages.RemoveAll(c => removeable.Any(f => f.FileName == f.FileName));
            ClothesImage main = new ClothesImage
            {
                Name = await clothes.MainPhoto.FileCreate(_env.WebRootPath, "assets/img"),
                IsMain = true,
                Alternative = clothes.Name,
                Clothes = clothes
            };
            clothes.ClothesImages.Add(main);

            await _context.Clothes.AddAsync(clothes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Information = _context.ClothesInformations.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            if (id == 0 || id == null) return NotFound();
            if (!ModelState.IsValid) return View();
            Clothes clothes = await _context.Clothes
                .Include(c => c.ClothesImages)
                .Include(c => c.ClothesInformation)
                .Include(c => c.Categories).SingleOrDefaultAsync(c => c.Id == id);
            if (clothes == null) return NotFound();
            return View(clothes);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, Clothes clothes)
        {
            if (id == 0 || id == null) return NotFound();
            Clothes existed = await _context.Clothes
               .Include(c => c.ClothesImages)
               .Include(c => c.ClothesInformation)
               .Include(c => c.Categories).FirstOrDefaultAsync(c => c.Id == id);
            if (!ModelState.IsValid) return View(existed);           
            if (existed == null) return NotFound();
            List<ClothesImage> clothesImages = existed.ClothesImages.Where(c => c.IsMain == false &&
            !clothes.ImagesId.Contains(c.Id)).ToList();

             _context.Entry(existed).CurrentValues.SetValues(clothes);
            existed.ClothesImages.RemoveAll(c => clothesImages.Any(r => c.Id == r.Id));

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Clothes clothes = await _context.Clothes.FindAsync(id);
            if (clothes == null) return NotFound();
            _context.Clothes.Remove(clothes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Clothes clothes = await _context.Clothes.FirstOrDefaultAsync(c => c.Id == id);
            if (clothes == null) return NotFound();
            return View(clothes);
        }
    }
}
