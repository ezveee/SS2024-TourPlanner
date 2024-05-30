using Business.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Business.Api;

// TODO AFTER DONE: rm main function and set bl back to lib
class Program
{
	static async Task Main(string[] args)
	{
		OrsDirectionsApi api = new OrsDirectionsApi();

		await api.GeocodeGetCoordinatesAsync("Höchstädtplatz 6, 1200 Wien");

		await api.DirectionsGetRouteAsync("Schwedenplatz, 1010 Wien", "Höchstädtplatz 6, 1200 Wien", "walking");
	}
}

#region STRUCT DECLARATIONS
public struct Coordinates
{
	public string lon;
	public string lat;
}

public struct Feature
{
	public Geometry geometry;
}

public struct Geometry
{
	public string[] coordinates;
}

public struct Root
{
	public Feature[] features;
}
#endregion

// TODO: scuffed, change this future me pls pls plspls 
public class TransportTypes
{
	public Dictionary<string, string> DTransportTypes = new Dictionary<string, string>()
	{
		{ "Walking",  "foot-walking" },
		{ "walking", "foot-walking" },

		{ "Car", "driving-car" },
		{ "car", "driving-car" },

		{ "Bike", "cycling-regular" },
		{ "bike", "cycling-regular" }
	};
}


public class OrsDirectionsApi
{
	private HttpClient _client;
	private static readonly string _apiKey = "5b3ce3597851110001cf62486bc559c2cd674064a1310170a99106a2"; // TODO: maybe move to a config file or smth like that????

	public OrsDirectionsApi()
	{
		
	}

	// call geocode to receive long + lat for a place, returns it to use to get a route, perhaps give direction
	public async Task<Coordinates> GeocodeGetCoordinatesAsync(string address)
	{
		Coordinates geocode = new Coordinates();

		string addressEncoded = HttpUtility.UrlEncode(address);

		using (_client = new HttpClient { BaseAddress = new Uri($"https://api.openrouteservice.org/geocode/search?api_key={_apiKey}&text={addressEncoded}") })
		{
			_client.DefaultRequestHeaders.Clear();
			_client.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json, application/geo+json, application/gpx+xml, img/png; charset=utf-8");

			using (var response = await _client.GetAsync(""))
			{
				string responseData = await response.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<Root>(responseData);

				//Console.WriteLine(data);

				// LONGITUDE FIRST, LATITUDE SECOND
				geocode.lon = data.features[0].geometry.coordinates[0];
				geocode.lat = data.features[0].geometry.coordinates[1];
			}
		}

		return geocode;
	}

	// call directions api to receive distance, duration, etc
	public async Task<Tour> DirectionsGetRouteAsync(string start, string end, string transportType)
	{
		Tour temp = new Tour();
		TransportTypes transportTypes = new TransportTypes();

		Coordinates startPoint = GeocodeGetCoordinatesAsync(start).Result;
		Coordinates endPoint = GeocodeGetCoordinatesAsync(end).Result;

		using (_client = new HttpClient { BaseAddress = new Uri($"https://api.openrouteservice.org/v2/directions/{transportTypes.DTransportTypes[transportType]}" +
																	$"?api_key={_apiKey}" +
																	$"&start={startPoint.lon},{startPoint.lat}" +
																	$"&end={endPoint.lon},{endPoint.lat}")})
		{
			_client.DefaultRequestHeaders.Clear();
			_client.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json, application/geo+json, application/gpx+xml, img/png; charset=utf-8");

			using (var response = await _client.GetAsync(""))
			{
				string responseData = await response.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject(responseData);

				Console.WriteLine(data);

				
			}
		}

		return temp;
	}
}
