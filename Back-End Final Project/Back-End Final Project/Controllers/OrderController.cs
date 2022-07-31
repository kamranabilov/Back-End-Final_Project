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
    [Authorize (Roles ="Member")]
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
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            List<BasketItem> basket = await _context.BasketItems.Include(o => o.AppUser)
                .Include(o => o.Clothes)               
                .Where(o => o.AppUserId == user.Id).ToListAsync();
            order.Date = DateTime.Now;           
            order.Price =default;
            order.TotalPrice = default;
            order.AppUser = user;           
            order.BasketItems = basket;
            foreach (BasketItem item in basket)
            {
                order.TotalPrice += item.Price * item.Quantity;
            }
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
