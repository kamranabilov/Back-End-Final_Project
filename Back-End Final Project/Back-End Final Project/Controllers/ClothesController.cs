using Back_End_Final_Project.DAL;
using Back_End_Final_Project.Models;
using Back_End_Final_Project.ViewModels;
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
        public IActionResult Shop()
        {
            HomeVM homeVM = new HomeVM
            {                
                Clothes = _context.Clothes.Include(c => c.ClothesImages).ToList()
            };
            return View(homeVM);
        }
        public IActionResult ShopDetail()
        {
            HomeVM homeVM = new HomeVM
            {
                Clothes = _context.Clothes.Include(c => c.ClothesImages).ToList()
            };
            return View(homeVM);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == 0 || id == null) return NotFound();
            Clothes clothes = await _context.Clothes.Include(c =>c.ClothesImages)
                .Include(c=>c.ClothesInformation)               
                .Include(c => c.Categories)
                .FirstOrDefaultAsync(c => c.Id == id);
            return View();
           
        }
        // Partial View
        public async Task<IActionResult> Partial()
        {
            List<Clothes> clothes = await _context.Clothes.Include(c => c.ClothesImages).ToListAsync();
            return PartialView("_ClothesPartialView", clothes);
        } 
    }
}
