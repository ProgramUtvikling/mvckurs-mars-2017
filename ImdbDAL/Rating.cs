using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ImdbDAL
{
	public class Rating
	{
		public int RatingId { get; set; }
		public int Vote { get; set; }

		public Movie Movie { get; set; }
	}
}
