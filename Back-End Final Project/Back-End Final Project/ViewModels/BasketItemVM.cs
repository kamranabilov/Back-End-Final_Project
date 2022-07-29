using Back_End_Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End_Final_Project.ViewModels
{
    public class BasketItemVM
    {
        public Clothes Clothes { get; set; }
        public int Quantity { get; set; }
    }
}
