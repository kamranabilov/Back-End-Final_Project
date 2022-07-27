using Back_End_Final_Project.DAL;
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
    }
}
