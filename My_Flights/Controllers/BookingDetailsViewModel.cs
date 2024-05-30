using System;

namespace My_Flights.Controllers
{
	public class BookingDetailsViewModel
	{
		public int BookingID { get; internal set; }
		public DateTime BookingDate { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FlightNumber { get; set; }
		public string DepartureAirport { get; set; }
		public string ArrivalAirport { get; set; }
		public DateTime DepartureDate { get; set; }
		public DateTime ArrivalDate { get; set; }
		public string Airline { get; set; }
		public decimal Price { get; set; }
		public string SeatNumber { get; set; }
		public bool Availability { get; set; }
	}
}