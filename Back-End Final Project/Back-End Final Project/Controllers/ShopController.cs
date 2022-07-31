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

    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Shop(int? id, int page=1)
        {
            if (id != 0 || id != null)
            {
                Category category = await _context.Categories
                    .Include(c => c.Clothes).ThenInclude(c => c.ClothesImages).Skip((page - 1) * 4).Take(4)
                    .FirstOrDefaultAsync(x => x.Id == id);
                ViewBag.CurentPage = page;
                ViewBag.TotalPage = Math.Ceiling((decimal)_context.Clothes.Count() / 4);
                if (category != null)
                {
                    if (category.Clothes.Count() != 0)
                    {
                        HomeVM home = new HomeVM
                        {
                            Clothes = category.Clothes
                        };
                        return View(home);
                    }
                    else
                    {
                        ViewBag.Message = "category";
                        return View();
                    }
                }
            }
            HomeVM homeVM = new HomeVM
            {
                Clothes = _context.Clothes.Include(c => c.ClothesImages).ToList()
            };
            return View(homeVM);
            //List<Clothes> clothes = await _context.Clothes.Include(x => x.ClothesImages).ToListAsync();
            //return View(clothes);          
        }
        public async Task<IActionResult> Empty(int? id)
        {          
            return View();
        }
    }
}
