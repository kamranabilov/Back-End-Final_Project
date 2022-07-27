using Back_End_Final_Project.DAL;
using Back_End_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Back_End_Final_Project.Controllers
{
    public class ClothesController : Controller
    {
        private readonly AppDbContext _context;
        public ClothesController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == 0 || id == null) return NotFound();
            Clothes clothes = await _context.Clothes.Include(c =>c.ClothesImages)
                .Include(c=>c.ClothesInformation)               
                .Include(c => c.Categories)
                .FirstOrDefaultAsync(c => c.Id == id);
            //ViewBag.Clothes = await _context.Clothes.Include(c=>c.ClothesImages).ToListAsync();
            return View();
            //// Relation Category start
            //List<Clothes> clothess = new List<Clothes>();
            //List<Clothes> clothesRange = new List<Clothes>();

            //foreach (var item in clothess)
            //{
            //    clothes = _context.Clothes.Where(c => c.categories.Any(z => z.CategoryId == item.CategoryId))
            //        .Include(c => c.ClothesImages).ToList();

            //    clothesRange.AddRange(clothess);
            //}
            ////ViewBag.Clothes = clothesRange.Distinct().ToList();           
            //return View(clothes);
            //// Relation Category end
        }
    }
}
