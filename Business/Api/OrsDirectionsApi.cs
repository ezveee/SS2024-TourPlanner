using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Api;

public struct Coordinates
{
	string lat;
	string lon;
}

public class OrsDirectionsApi
{
	private static readonly HttpClient _client = new HttpClient();
	private static readonly string _apiKey = "5b3ce3597851110001cf62486bc559c2cd674064a1310170a99106a2"; // TODO: maybe move to a config file or smth like that????

	public OrsDirectionsApi()
	{
		
	}

	// call geocode to receive long + lat for a place, returns it to use to get a route, perhaps give direction
	public Coordinates GeocodeGetCoordinates(string address)
	{
		Coordinates geocode = new Coordinates();

		// call api here, parse coordinates from json res

		return geocode;
	}

	// 
	public Tour DirectionsGetRoute(string start, string end)
	{
		Tour temp = new Tour();

		Coordinates startPoint = GeocodeGetCoordinates(start);
		Coordinates endPoint = GeocodeGetCoordinates(end);

		return temp;
	}
}
