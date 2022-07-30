using Back_End_Final_Project.DAL;
using Back_End_Final_Project.Models;
using Back_End_Final_Project.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Back_End_Final_Project.Controllers
{
    public class ClothesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ClothesController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Detail(int? id)
        {

            if (id == 0 || id == null) return NotFound();
            Clothes clothes = await _context.Clothes.Include(c =>c.ClothesImages)
                .Include(c=>c.ClothesInformation)                               
                .FirstOrDefaultAsync(c => c.Id == id);
            return View(clothes);
           
        }
        // Partial View
        public async Task<IActionResult> Partial()
        {
            List<Clothes> clothes = await _context.Clothes.Include(c => c.ClothesImages).ToListAsync();
            return PartialView("_ClothesPartialView", clothes);
        }

       
    }
}
