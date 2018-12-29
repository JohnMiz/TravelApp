using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.Helpers;

namespace TravelApp.Core.Models
{
	 public class Location
	 {
		  public string address { get; set; }
		  public string crossStreet { get; set; }
		  public double lat { get; set; }
		  public double lng { get; set; }
		  public int distance { get; set; }
		  public string postalCode { get; set; }
		  public string cc { get; set; }
		  public string city { get; set; }
		  public string state { get; set; }
		  public string country { get; set; }
		  public IList<string> formattedAddress { get; set; }
	 }

	 public class Category
	 {
		  public string id { get; set; }
		  public string name { get; set; }
		  public string pluralName { get; set; }
		  public string shortName { get; set; }
		  public bool primary { get; set; }
	 }

	 public class Venue
	 {
		  public string id { get; set; }
		  public string name { get; set; }
		  public Location location { get; set; }
		  public IList<Category> categories { get; set; }

		  public static async Task<List<Venue>> GetVenues(double latitude, double longtitude)
		  {
			   List<Venue> venues = new List<Venue>();

			   string url = VenueRoot.GenerateURL(latitude, longtitude);

			   using (HttpClient client = new HttpClient())
			   {
					HttpResponseMessage response = await client.GetAsync(url);

					string venuesJson = await response.Content.ReadAsStringAsync();

					var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(venuesJson);

					if (venueRoot != null)
					{
						 venues = venueRoot.response.venues as List<Venue>;
					}

			   }

			   return venues;
		  }
	 }

	 public class Response
	 {
		  public IList<Venue> venues { get; set; }
	 }

	 public class VenueRoot
	 {
		  public Response response { get; set; }

		  public static string GenerateURL(double latitude, double longtitude)
		  {
			   return string.Format(Constants.VENUE_SEARCH, latitude, longtitude, Constants.CLIENT_ID, Constants.CLIENT_SECRET, DateTime.Now.ToString("yyyyMMdd"));
		  }

	 }
}
