using Back_End_Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End_Final_Project.ViewModels
{
    public class LayoutBasketVM
    {
        public List<BasketItemVM> BasketItemVMs { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
