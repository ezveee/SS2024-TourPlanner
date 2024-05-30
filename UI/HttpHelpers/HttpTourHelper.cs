using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using UI.Http;
using UI.Interfaces;
using UI.Models;

namespace UI.HttpHelpers;
public class HttpTourHelper : IHttpHelper<TourModel>
{
	private readonly JsonSerializerOptions _options = new()
	{
		DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
	};

	public async Task<TourModel?> CreateDataAsync(TourModel tour)
	{
		try
		{
			string url = Resource.Route_Tour;
			string json = JsonSerializer.Serialize(tour, _options);
			HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await HttpClientSingleton.Instance.PostAsync(url, content);
			_ = response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();

			_ = MessageBox.Show(responseBody);
			return JsonSerializer.Deserialize<TourModel>(responseBody);
		}
		catch (HttpRequestException e)
		{
			_ = MessageBox.Show("Request error: " + e.Message);
			return null;
		}
	}

	public async Task<List<TourModel>?> ReadDataAsync()
	{
		try
		{
			string url = Resource.Route_Tour;
			HttpResponseMessage response = await HttpClientSingleton.Instance.GetAsync(url);
			_ = response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();
			_ = MessageBox.Show(responseBody);
			return JsonSerializer.Deserialize<List<TourModel>>(responseBody);
		}
		catch (HttpRequestException e)
		{
			_ = MessageBox.Show("Request error: " + e.Message);
			return null;
		}
	}

	public async Task<TourModel?> ReadDataAsync(int id)
	{
		try
		{
			string url = Resource.Route_Tour + $"/{id}";
			HttpResponseMessage response = await HttpClientSingleton.Instance.GetAsync(url);
			_ = response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();
			_ = MessageBox.Show(responseBody);
			return JsonSerializer.Deserialize<TourModel>(responseBody);
		}
		catch (HttpRequestException e)
		{
			_ = MessageBox.Show("Request error: " + e.Message);
			return null;
		}
	}

	public async Task<TourModel?> UpdateDataAsync(TourModel updatedTour)
	{
		try
		{
			string url = Resource.Route_Tour;
			string json = JsonSerializer.Serialize(updatedTour, _options);
			HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await HttpClientSingleton.Instance.PutAsync(url, content);
			_ = response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();
			_ = MessageBox.Show(responseBody);
			return JsonSerializer.Deserialize<TourModel>(responseBody);
		}
		catch (HttpRequestException e)
		{
			_ = MessageBox.Show("Request error: " + e.Message);
			return null;
		}
	}

	public async Task DeleteDataAsync(int id)
	{
		try
		{
			string url = Resource.Route_Tour + $"/{id}";
			HttpResponseMessage response = await HttpClientSingleton.Instance.DeleteAsync(url);
			_ = response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();
			_ = MessageBox.Show(responseBody);
		}
		catch (HttpRequestException e)
		{
			_ = MessageBox.Show("Request error: " + e.Message);
		}
	}
}
