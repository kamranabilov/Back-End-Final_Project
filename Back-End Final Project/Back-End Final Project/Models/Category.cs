﻿using Back_End_Final_Project.Models.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End_Final_Project.Models
{
    public class Category:BaseEntity
    {
        [Required, StringLength(maximumLength:30)]
        public string Image { get; set; }
        public string Name { get; set; }
        public byte Quantity { get; set; }
        public List<Clothes> Clothes { get; set; }
        [NotMapped]
        public IFormFile Picture { get; set; }
    }
}
