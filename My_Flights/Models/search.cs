using My_Flights.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace My_Flights.Models
{
	public class SearchViewModel
	{
		public string SearchQuery { get; set; }
		public List<BookingDetailsViewModel> SearchResults { get; set; }

		
	}

}