﻿using Newtonsoft.Json;
using System.Web;

namespace Business.Api;

// TODO AFTER DONE: rm main function and set bl back to lib
internal class Program
{
	private static void Main(string[] args)
	{
		OrsDirectionsApi api = new();

		_ = api.GeocodeGetCoordinatesAsync("Höchstädtplatz 6, 1200 Wien").Result;

		_ = api.DirectionsGetRouteAsync("Schwedenplatz, 1010 Wien", "Höchstädtplatz 6, 1200 Wien", "walking").Result;
	}
}

public struct Coordinates
{
	public string lon;
	public string lat;
}

#region STRUCT DECLARATIONS GEOCODE
public struct Feature
{
	public Geometry geometry;
}

public struct Geometry
{
	public string[] coordinates;
}

public struct GeocodeRoot
{
	public Feature[] features;
}
#endregion

#region STRUCT DECLARATIONS DIRECTIONS
public struct Summary
{
	public float distance;
	public double duration;
}

public struct Properties
{
	public Summary summary;
}

public struct Route // == Feature
{
	public Properties properties;
}

public struct DirectionsRoot
{
	public List<Route> features;
}
#endregion

// TODO: scuffed, change this future me pls pls plspls 
public class TransportTypes
{
	public Dictionary<string, string> DTransportTypes = new()
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

	private readonly OsmTilesApi _osm = new();

	public OrsDirectionsApi()
	{

	}

	// call geocode to receive long + lat for a place, returns it to use to get a route, perhaps give direction
	public async Task<Coordinates> GeocodeGetCoordinatesAsync(string address)
	{
		Coordinates geocode = new();

		string addressEncoded = HttpUtility.UrlEncode(address);

		using (_client = new HttpClient { BaseAddress = new Uri($"https://api.openrouteservice.org/geocode/search?api_key={_apiKey}&text={addressEncoded}") })
		{
			_client.DefaultRequestHeaders.Clear();
			_ = _client.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json, application/geo+json, application/gpx+xml, img/png; charset=utf-8");

			using HttpResponseMessage response = await _client.GetAsync("");
			if (!response.IsSuccessStatusCode)
			{
				// Console.WriteLine(response.StatusCode.ToString());

				throw new Exception($"Failed to retrieve coordinates for {address}");
			}

			string responseData = await response.Content.ReadAsStringAsync();
			GeocodeRoot data = JsonConvert.DeserializeObject<GeocodeRoot>(responseData);

			//Console.WriteLine(data);

			// LONGITUDE FIRST, LATITUDE SECOND
			geocode.lon = data.features[0].geometry.coordinates[0];
			geocode.lat = data.features[0].geometry.coordinates[1];
		}

		return geocode;
	}

	// call directions api to receive distance, duration, etc
	public async Task<(float Distance, double Duration, string Image)> DirectionsGetRouteAsync(string start, string end, string transportType)
	{
		TransportTypes transportTypes = new();

		Coordinates startPoint = GeocodeGetCoordinatesAsync(start).Result;
		Coordinates endPoint = GeocodeGetCoordinatesAsync(end).Result;

		string image = await _osm.TilesCreateMapPngAsync(startPoint);

		// Console.WriteLine(startPoint.lon + " " + endPoint.lon);

		using (_client = new HttpClient
		{
			BaseAddress = new Uri($"https://api.openrouteservice.org/v2/directions/{transportTypes.DTransportTypes[transportType]}" +
																	$"?api_key={_apiKey}" +
																	$"&start={startPoint.lon},{startPoint.lat}" +
																	$"&end={endPoint.lon},{endPoint.lat}")
		})
		{
			_client.DefaultRequestHeaders.Clear();
			_ = _client.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json, application/geo+json, application/gpx+xml, img/png; charset=utf-8");

			using HttpResponseMessage response = await _client.GetAsync("");
			if (!response.IsSuccessStatusCode)
			{
				// Console.WriteLine(response.StatusCode.ToString());

				throw new Exception($"Failed to retrieve route");
			}

			string responseData = await response.Content.ReadAsStringAsync();

			DirectionsRoot data = JsonConvert.DeserializeObject<DirectionsRoot>(responseData);

			return ((float)Math.Round(data.features[0].properties.summary.distance / 1000, 2), Math.Round(data.features[0].properties.summary.duration / 60, 2), image);
		}
	}
}
