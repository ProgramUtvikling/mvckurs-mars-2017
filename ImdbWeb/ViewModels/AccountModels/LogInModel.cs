using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWeb.ViewModels.AccountModels
{
    public class LogInModel
    {
        [Required, StringLength(10, MinimumLength =3)]
        [Display(Name = "Brukernavn")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Passord")]
        public string Password { get; set; }

        [Display(Name = "Husk meg på denne maskinen")]
        public bool RememberMe { get; set; }
    }
}
