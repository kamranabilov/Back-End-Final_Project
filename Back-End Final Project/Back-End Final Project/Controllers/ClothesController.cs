using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End_Final_Project.Controllers
{
    public class ClothesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
