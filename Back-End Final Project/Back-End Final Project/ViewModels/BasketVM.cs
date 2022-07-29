using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End_Final_Project.ViewModels
{
    public class BasketVM
    {
        public List<BasketCookieItemVM> BasketCookieItemVMs { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
