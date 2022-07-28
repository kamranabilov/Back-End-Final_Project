using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End_Final_Project.ViewModels
{
    public class LoginVM
    {
        [Required, StringLength(20)]
        public string UserName { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}
