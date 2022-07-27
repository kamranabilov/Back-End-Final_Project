using Back_End_Final_Project.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End_Final_Project.Models
{
    public class ClothesInformation:BaseEntity
    {
        public string Information { get; set; }
        public List<Clothes> Clothes { get; set; }
    }
}
