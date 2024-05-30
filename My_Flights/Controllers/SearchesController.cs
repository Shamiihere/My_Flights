using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using My_Flights.Models;

namespace My_Flights.Controllers
{
	public class SearchesController : Controller
	{
		public FlightEntities db = new FlightEntities();

		// GET: Searches
		public ActionResult Index()
		{
			return View(new SearchViewModel());
		}

		[HttpPost]
		public ActionResult SearchResults(string searchQuery)
		{
			string sqlQuery = @"
            SELECT 
                B.BookingID,
                B.BookingDate,
                P.FirstName,
                P.LastName,
                F.FlightNumber,
                F.DepartureAirport,
                F.ArrivalAirport,
                F.DepartureDate,
                F.ArrivalDate,
                F.Airline,
                F.Price,
                S.SeatNumber,
                S.Availability
            FROM 
                Booking AS B
            INNER JOIN 
                Passenger AS P ON B.PassengerID = P.PassengerID
            INNER JOIN 
                Flight AS F ON B.FlightID = F.FlightID
            INNER JOIN 
                Seat AS S ON F.FlightID = S.FlightID
            WHERE 
                B.BookingID = @searchQuery
                OR P.FirstName LIKE '%' + @searchQuery + '%'
                OR P.LastName LIKE '%' + @searchQuery + '%'
        ";

			string connectionString = "Data Source=DESKTOP-OKRVMSA\\MYONE;Initial Catalog=Flight;Integrated Security=True;";
			List<BookingDetailsViewModel> searchResults = new List<BookingDetailsViewModel>();

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(sqlQuery, connection);
				command.Parameters.AddWithValue("@searchQuery", searchQuery);
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					BookingDetailsViewModel bookingDetails = new BookingDetailsViewModel
					{
						BookingID = Convert.ToInt32(reader["BookingID"]),
						BookingDate = Convert.ToDateTime(reader["BookingDate"]),
						FirstName = reader["FirstName"].ToString(),
						LastName = reader["LastName"].ToString(),
						FlightNumber = reader["FlightNumber"].ToString(),
						DepartureAirport = reader["DepartureAirport"].ToString(),
						ArrivalAirport = reader["ArrivalAirport"].ToString(),
						DepartureDate = Convert.ToDateTime(reader["DepartureDate"]),
						ArrivalDate = Convert.ToDateTime(reader["ArrivalDate"]),
						Airline = reader["Airline"].ToString(),
						Price = Convert.ToDecimal(reader["Price"]),
						SeatNumber = reader["SeatNumber"].ToString(),
						Availability = Convert.ToBoolean(reader["Availability"])


					};
					searchResults.Add(bookingDetails);
				}
			}

			var viewModel = new SearchViewModel
			{
				SearchQuery = searchQuery,
				SearchResults = searchResults
			};

			return View("Index", viewModel);
		}
	}
}
