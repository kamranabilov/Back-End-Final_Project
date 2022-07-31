﻿using Back_End_Final_Project.DAL;
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
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CartController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Cart()
        {
            return View();
        }
        public async Task<IActionResult> AddBasket(int? id)
        {
            Clothes clothes = await _context.Clothes.FirstOrDefaultAsync(c => c.Id == id);
            if (id == null || id == 0) return NotFound();

            if (User.Identity.IsAuthenticated && User.IsInRole("Member"))
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
                BasketStr = JsonConvert.SerializeObject(basket);

                HttpContext.Response.Cookies.Append("Basket", BasketStr);
            }
            //return Json("working");
            //return Redirect(Request.Headers["Referer"].ToString());

            return RedirectToAction("cart", "cart");
        }
        public IActionResult ShowBasket()
        {
            if (HttpContext.Request.Cookies["Basket"] == null) return NotFound();
            BasketVM basket = JsonConvert.DeserializeObject<BasketVM>(HttpContext.Request.Cookies["Basket"]);
            return Json(basket);
        }
        public IActionResult Plus(int? id)
        {
            if (id == 0 || id == null) return NotFound();
            string basketStr = HttpContext.Request.Cookies["Basket"];
            Clothes clothes = _context.Clothes.FirstOrDefault(c => c.Id == id);
            BasketVM basket;
            if (basketStr == null)
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

                basket = JsonConvert.DeserializeObject<BasketVM>(basketStr);

                BasketCookieItemVM cookieItemVM = basket.BasketCookieItemVMs.SingleOrDefault(c => c != null && c.Id == id);
                if (cookieItemVM == null)
                {
                    BasketCookieItemVM basketCookieItemVM = new BasketCookieItemVM
                    {
                        Id = clothes.Id,
                        Quantity = 1

                    };
                    basket.BasketCookieItemVMs.Add(basketCookieItemVM);
                    basket.TotalPrice += clothes.Price;
                }
                else
                {
                    basket.TotalPrice += clothes.Price;
                    cookieItemVM.Quantity++;
                }
            }
            basketStr = JsonConvert.SerializeObject(basket);
            HttpContext.Response.Cookies.Append("Basket", basketStr);
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Minus(int? id)
        {
            if (id == 0 || id == null) return NotFound();
            string basketStr = HttpContext.Request.Cookies["Basket"];
            Clothes clothes = _context.Clothes.FirstOrDefault(c => c.Id == id);
            BasketVM basket;
            if (basketStr == null)
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
                basket = JsonConvert.DeserializeObject<BasketVM>(basketStr);
                BasketCookieItemVM cookieItemVM = basket.BasketCookieItemVMs.SingleOrDefault(c => c != null && c.Id == id);
                if (cookieItemVM == null)
                {
                    BasketCookieItemVM basketCookieItemVM = new BasketCookieItemVM
                    {
                        Id = clothes.Id,
                        Quantity = 1
                    };
                    basket.BasketCookieItemVMs.Add(basketCookieItemVM);
                    basket.TotalPrice += clothes.Price;
                }
                else
                {                   
                    if (cookieItemVM.Quantity>1)
                    {
                        basket.TotalPrice -= clothes.Price;
                        cookieItemVM.Quantity--;
                    }                  
                }
            }
            basketStr = JsonConvert.SerializeObject(basket);
            HttpContext.Response.Cookies.Append("Basket", basketStr);
            return RedirectToAction("cart", "cart");
            //return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> RemoveFromClothes(int? id)
        {
            if (id == null || id == 0) NotFound();
            Clothes clothes = await _context.Clothes.FirstOrDefaultAsync(c => c.Id == id);
            if (clothes == null) NotFound();
            string basketStr = HttpContext.Request.Cookies["Basket"];
            if (string.IsNullOrEmpty(basketStr)) NotFound();
            BasketVM basket = JsonConvert.DeserializeObject<BasketVM>(basketStr);
            BasketCookieItemVM existed = basket.BasketCookieItemVMs.FirstOrDefault(c => c.Id == id);
            basket.BasketCookieItemVMs.Remove(existed);
            basket.TotalPrice -= (existed.Quantity * clothes.Price);
            basketStr = JsonConvert.SerializeObject(basket);
            HttpContext.Response.Cookies.Append("Basket", basketStr);
            return RedirectToAction("cart", "cart");
        }
    }
}
