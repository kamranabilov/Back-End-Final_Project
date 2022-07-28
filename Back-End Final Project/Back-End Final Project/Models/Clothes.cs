using Back_End_Final_Project.Models.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End_Final_Project.Models
{
    public class Clothes:BaseEntity
    {
        public string Image { get; set; }
        [Required, StringLength(maximumLength:20)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Text { get; set; }      
        public string Description{ get; set; }      
        public int ClothesInformationId { get; set; }
        public ClothesInformation ClothesInformation { get; set; }
        public int CategoiesId { get; set; }
        public Category Categories { get; set; }
        public List<ClothesImage> ClothesImages { get; set; }
        [NotMapped]
        public IFormFile MainPhoto { get; set; }
        [NotMapped]
        public List<IFormFile> Photos { get; set; }
        [NotMapped]
        public List<int> ImagesId { get; set; }
    }
}
