using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWeb.ViewModels
{
    public class LogInModel
    {
        [Display(Name = "Brukernavn")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Passord")]
        public string Password { get; set; }

        [Display(Name = "Husk meg på denne maskinen")]
        public bool RememberMe { get; set; }
    }
}
