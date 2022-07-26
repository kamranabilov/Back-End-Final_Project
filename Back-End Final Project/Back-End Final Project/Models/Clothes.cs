using Back_End_Final_Project.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End_Final_Project.Models
{
    public class Clothes:BaseEntity
    {
        public string Image { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Text { get; set; }      
        public string Description{ get; set; }      
        public int ClothesInformationId { get; set; }
        public ClothesInformation ClothesInformation { get; set; }
        public int CategoiesId { get; set; }
        public Category Categories { get; set; }
        public List<ClothesImage> ClothesImages { get; set; }
        //public List<ClothesColor> ClothesColors { get; set; }
        //public List<ClothesSize> ClothesSizes { get; set; }
    }
}
