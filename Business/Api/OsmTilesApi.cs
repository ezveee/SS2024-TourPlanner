using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Api;

//class Program
//{
//	static void Main(string[] args)
//	{
//		OsmTilesApi osmTilesApi = new OsmTilesApi();
//		Coordinates coordinates = new Coordinates()
//		{
//			lat = "48.211258",
//			lon = "16.378669"
//		};

//		_ = osmTilesApi.TilesCreateMapPngAsync(coordinates).Result;
//	}
//}

internal class OsmTilesApi
{
	private HttpClient _client;
	private readonly int _zoom = 19;

	public OsmTilesApi()
	{
		
	}

	public async Task<string> TilesCreateMapPngAsync(Coordinates coordinates)
	{
		double.TryParse(coordinates.lat.Replace(".", ","), out double lat);
		double.TryParse(coordinates.lon.Replace(".", ","), out double lon);

		(int x, int y) = ConvertCoordinatesToTiles(lat, lon);

		using (_client = new HttpClient())
		{
			_client.DefaultRequestHeaders.UserAgent.ParseAdd("tourplanner");

			using (var response = await _client.GetAsync($"https://tile.openstreetmap.org/{_zoom}/{x}/{y}.png", HttpCompletionOption.ResponseHeadersRead))
			{
				if (!response.IsSuccessStatusCode)
				{
					// Console.WriteLine(response.StatusCode.ToString());

					throw new Exception("Failed to retrieve map tile");
				}

				var bytes = await response.Content.ReadAsByteArrayAsync();

				string directoryPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Images");
				if (!Directory.Exists(directoryPath))
				{
					Directory.CreateDirectory(directoryPath);
				}

				string imageName = $"{Guid.NewGuid()}.png";
				string imagePath = Path.Combine(directoryPath, imageName);
				await File.WriteAllBytesAsync(imagePath, bytes);
				return imageName;
			}
		}
	}

	private (int x, int y) ConvertCoordinatesToTiles(double lat, double lon)
	{
		int n = (int)Math.Pow(2, _zoom);

		int x = (int)(n * ((lon + 180) / 360));
		int y = (int)(n * (1 - (Math.Log(Math.Tan(lat * Math.PI / 180) + 1 / Math.Cos(lat * Math.PI / 180)) / Math.PI) / 2));

		return (x, y);
	}
}
