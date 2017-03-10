using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWeb.ViewModels.MovieAdminModels
{
    public class MovieAdminCreateModel
    {
        [CustomValidation(typeof(ImdbWeb.Controllers.MovieAdminController), "CheckId")]
        [Required]
        [MaxLength(30)]
        public string MovieId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(100)]
        public string OriginalTitle { get; set; }

        public string Description { get; set; }

        [MaxLength(4)]
        public string ProductionYear { get; set; }

        [Range(0, (int.MaxValue/60)-1)]
        public int RunningLengthHours { get; set; }

        [Range(0,59)]
        public int RunningLengthMinutes { get; set; }

        [Required]
        public int GenreId { get; set; }

        //public virtual Genre Genre { get; set; }
    }
}
