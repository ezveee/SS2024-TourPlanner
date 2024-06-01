﻿using Business.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Business.Api;

// TODO AFTER DONE: rm main function and set bl back to lib
//class Program
//{
//	static void Main(string[] args)
//	{
//		OrsDirectionsApi api = new OrsDirectionsApi();

//		_ = api.GeocodeGetCoordinatesAsync("Höchstädtplatz 6, 1200 Wien").Result;

//		_ = api.DirectionsGetRouteAsync("Schwedenplatz, 1010 Wien", "Höchstädtplatz 6, 1200 Wien", "walking").Result;
//	}
//}

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
				if (!response.IsSuccessStatusCode)
				{
					// Console.WriteLine(response.StatusCode.ToString());

					throw new Exception($"Failed to retrieve coordinates for {address}");
				}

				string responseData = await response.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<GeocodeRoot>(responseData);

				//Console.WriteLine(data);

				// LONGITUDE FIRST, LATITUDE SECOND
				geocode.lon = data.features[0].geometry.coordinates[0];
				geocode.lat = data.features[0].geometry.coordinates[1];
			}
		}

		return geocode;
	}

	// call directions api to receive distance, duration, etc
	public async Task<(float Distance, double Duration)> DirectionsGetRouteAsync(string start, string end, string transportType)
	{
		TransportTypes transportTypes = new TransportTypes();

		Coordinates startPoint = GeocodeGetCoordinatesAsync(start).Result;
		Coordinates endPoint = GeocodeGetCoordinatesAsync(end).Result;

		// Console.WriteLine(startPoint.lon + " " + endPoint.lon);

		using (_client = new HttpClient { BaseAddress = new Uri($"https://api.openrouteservice.org/v2/directions/{transportTypes.DTransportTypes[transportType]}" +
																	$"?api_key={_apiKey}" +
																	$"&start={startPoint.lon},{startPoint.lat}" +
																	$"&end={endPoint.lon},{endPoint.lat}")})
		{
			_client.DefaultRequestHeaders.Clear();
			_client.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json, application/geo+json, application/gpx+xml, img/png; charset=utf-8");

			using (var response = await _client.GetAsync(""))
			{
				if (!response.IsSuccessStatusCode)
				{
					// Console.WriteLine(response.StatusCode.ToString());

					throw new Exception($"Failed to retrieve route");
				}

				string responseData = await response.Content.ReadAsStringAsync();

				var data = JsonConvert.DeserializeObject<DirectionsRoot>(responseData);

				return ((float)data.features[0].properties.summary.distance, (double)data.features[0].properties.summary.duration);
			}
		}
	}
}
