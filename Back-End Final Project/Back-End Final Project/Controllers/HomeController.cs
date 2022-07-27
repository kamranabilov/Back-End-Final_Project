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
    public class HomeController : Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Sliders = _context.Sliders.ToList(),
                Categories = _context.Categories.Include(c => c.Clothes).ToList(),
                Clothes = _context.Clothes.Include(c => c.ClothesImages).ToList()
            };
             return View(homeVM);
        }
    }
}
