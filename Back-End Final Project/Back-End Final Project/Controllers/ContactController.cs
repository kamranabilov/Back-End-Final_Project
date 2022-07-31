using Back_End_Final_Project.DAL;
using Back_End_Final_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End_Final_Project.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ContactController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }        
                [HttpPost]
            public async Task<IActionResult> Index(ContactUs contactUs)
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (!ModelState.IsValid) return View();
                    AppUser user = await _userManager.FindByNameAsync(contactUs.Name);
                    if (user != null)
                    {
                        ContactUs contact = new ContactUs
                        {
                            Name = contactUs.Name,
                            Email = contactUs.Email,
                            Subject = contactUs.Subject,
                            Message = contactUs.Message
                        };
                        await _context.ContactUss.AddAsync(contact);
                        await _context.SaveChangesAsync();
                        return View();
                    }
                    else
                    {
                        ModelState.AddModelError("Contact", "Erorr Msj");
                        return View();
                    }
                }
                return RedirectToAction(nameof(Index));
            }        
    }
}
