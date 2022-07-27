using Back_End_Final_Project.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End_Final_Project.Models
{
    public class Size:BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
