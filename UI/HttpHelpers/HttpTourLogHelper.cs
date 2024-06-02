using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using UI.Http;
using UI.Interfaces;
using UI.Models;

namespace UI.HttpHelpers;
public class HttpTourLogHelper : IHttpHelper<TourLogModel>
{
	private readonly JsonSerializerOptions _options = new()
	{
		DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
	};

	public async Task<TourLogModel?> CreateDataAsync(TourLogModel tourLog)
	{
		try
		{
			string url = Resource.Route_TourLog;
			string json = JsonSerializer.Serialize(tourLog, _options);
			HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await HttpClientSingleton.Instance.PostAsync(url, content);
			_ = response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();

			return JsonSerializer.Deserialize<TourLogModel>(responseBody);
		}
		catch (HttpRequestException e)
		{
			_ = MessageBox.Show("Request error: " + e.Message);
			return null;
		}
	}

	public async Task<List<TourLogModel>?> GetDataAsync()
	{
		try
		{
			string url = Resource.Route_TourLog;
			HttpResponseMessage response = await HttpClientSingleton.Instance.GetAsync(url);
			_ = response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<List<TourLogModel>>(responseBody);
		}
		catch (HttpRequestException e)
		{
			_ = MessageBox.Show("Request error: " + e.Message);
			return null;
		}
	}

	public async Task<TourLogModel?> GetDataAsync(int id)
	{
		try
		{
			string url = Resource.Route_TourLog + $"/logs/{id}";
			HttpResponseMessage response = await HttpClientSingleton.Instance.GetAsync(url);
			_ = response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<TourLogModel>(responseBody);
		}
		catch (HttpRequestException e)
		{
			_ = MessageBox.Show("Request error: " + e.Message);
			return null;
		}
	}

	public async Task<TourLogModel?> UpdateDataAsync(TourLogModel updatedTourLog)
	{
		try
		{
			string url = Resource.Route_TourLog;
			string json = JsonSerializer.Serialize(updatedTourLog, _options);
			HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await HttpClientSingleton.Instance.PutAsync(url, content);
			_ = response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<TourLogModel>(responseBody);
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
			string url = Resource.Route_TourLog + $"/{id}";
			HttpResponseMessage response = await HttpClientSingleton.Instance.DeleteAsync(url);
			_ = response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();
		}
		catch (HttpRequestException e)
		{
			_ = MessageBox.Show("Request error: " + e.Message);
		}
	}
}
