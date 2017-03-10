using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbWeb.ViewModels.MovieAdminModels
{
    public class MovieAdminEditModel
    {
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(100)]
        public string OriginalTitle { get; set; }

        public string Description { get; set; }

        [MaxLength(4)]
        public string ProductionYear { get; set; }

        public int RunningLengthHours { get; set; }
        public int RunningLengthMinutes { get; set; }

        public int GenreId { get; set; }

        //public virtual Genre Genre { get; set; }
    }
}
