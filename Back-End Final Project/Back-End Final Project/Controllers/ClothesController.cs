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
                .Include(c => c.Categories)
                .FirstOrDefaultAsync(c => c.Id == id);
            return View(clothes);
           
        }
        // Partial View
        public async Task<IActionResult> Partial()
        {
            List<Clothes> clothes = await _context.Clothes.Include(c => c.ClothesImages).ToListAsync();
            return PartialView("_ClothesPartialView", clothes);
        }

        //Basket
        public async Task<IActionResult> AddBasket(int? id)
        {
            Clothes clothes = await _context.Clothes.FirstOrDefaultAsync(c => c.Id == id);
            if (id == null || id == 0) return NotFound();

            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null) return NotFound();
                BasketItem existed = await _context.BasketItems
                    .FirstOrDefaultAsync(b => b.AppUserId == user.Id && b.ClothesId == clothes.Id);
                if (existed == null)
                {
                    existed = new BasketItem
                    {
                        Clothes = clothes,
                        AppUser = user,
                        Quantity = 1,
                        Price = clothes.Price
                    };
                    _context.BasketItems.Add(existed);
                }
                else
                {
                    existed.Quantity++;
                }
                await _context.SaveChangesAsync();
            }
            else
            {
                if (clothes == null) return NotFound();
                string BasketStr = HttpContext.Request.Cookies["Basket"];

                BasketVM basket;
                if (string.IsNullOrEmpty(BasketStr))
                {
                    basket = new BasketVM();
                    BasketCookieItemVM cookieItemVM = new BasketCookieItemVM
                    {
                        Id = clothes.Id,
                        Quantity = 1
                    };
                    basket.BasketCookieItemVMs = new List<BasketCookieItemVM>();
                    basket.BasketCookieItemVMs.Add(cookieItemVM);
                    basket.TotalPrice = clothes.Price;
                }
                else
                {
                    basket = JsonConvert.DeserializeObject<BasketVM>(BasketStr);
                    BasketCookieItemVM existed = basket.BasketCookieItemVMs.FirstOrDefault(b => b.Id == id);
                    if (existed == null)
                    {
                        BasketCookieItemVM cookieItemVM = new BasketCookieItemVM
                        {
                            Id = clothes.Id,
                            Quantity = 1
                        };
                        basket.BasketCookieItemVMs.Add(cookieItemVM);
                        basket.TotalPrice += clothes.Price;
                    }
                    else
                    {
                        basket.TotalPrice += clothes.Price;
                        existed.Quantity++;
                    }
                }

                foreach (BasketCookieItemVM cookie in basket.BasketCookieItemVMs)
                {

                }
                BasketStr = JsonConvert.SerializeObject(basket);

                HttpContext.Response.Cookies.Append("Basket", BasketStr);
            }

            return RedirectToAction("Index","Home");            
        }

        public IActionResult ShowBasket()
        {
            if (HttpContext.Request.Cookies["Basket"] == null) return NotFound();
            BasketVM basket = JsonConvert.DeserializeObject<BasketVM>(HttpContext.Request.Cookies["Basket"]);
            return Json(basket);
        }
    }
}
