using Back_End_Final_Project.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End_Final_Project.Models
{
    public class BasketItem:BaseEntity
    {
        public int ClothesId { get; set; }
        public Clothes Clothes { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ColorId { get; set; }
        public Color Color { get; set; }
        public int SizeId { get; set; }
        public Size Size { get; set; }       
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
