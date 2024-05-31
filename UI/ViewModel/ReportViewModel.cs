using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UI.Http;
using UI.Logging;

namespace UI.ViewModel;
public class ReportViewModel
{
	private static readonly ILogger _logger = LoggerFactory.GetLogger();

	public async Task PostDataToApiAsync(int id, string type)
	{
		try
		{
			string url = "/report";
			// Create JSON payload with the ID
			string json = $"{{ \"id\": {id}, \"type\": \"{type}\" }}";
			HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

			_logger.Info("Sending request to generate report");
			HttpResponseMessage response = await HttpClientSingleton.Instance.PostAsync(url, content);
			response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();
			// Process the response
			MessageBox.Show(responseBody);
			_logger.Info("Report was successfully generated");
		}
		catch (HttpRequestException e)
		{
			// Handle request error
			MessageBox.Show("Request error: " + e.Message);
			_logger.Error("Request error: " + e.Message);
		}
	}
}
