using Back_End_Final_Project.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End_Final_Project.Models
{
    public class Clothes:BaseEntity
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Desc { get; set; }
        public int ClothesDescId { get; set; }
        public ClothesDesc ClothesDesc { get; set; }    
        public int ClothesInformationId { get; set; }
        public ClothesInformation ClothesInformation { get; set; }
        public List<ClothesImage> ClothesImages { get; set; }
    }
}
