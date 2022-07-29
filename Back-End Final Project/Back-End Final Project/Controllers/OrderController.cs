using Back_End_Final_Project.DAL;
using Back_End_Final_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Back_End_Final_Project.Controllers
{
    [Authorize(Roles ="Admin")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Checkout(Order order)
        {
            if (!ModelState.IsValid) return View();            
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            List<BasketItem> items = await _context.BasketItems.Include(b => b.AppUser)
                .Include(b => b.Clothes).Where(b => b.AppUser.Id == user.Id).ToListAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
