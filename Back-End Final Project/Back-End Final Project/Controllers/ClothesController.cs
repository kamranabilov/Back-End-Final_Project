using Back_End_Final_Project.DAL;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Detail(int? id)
        {
            return View();
        }
    }
}
