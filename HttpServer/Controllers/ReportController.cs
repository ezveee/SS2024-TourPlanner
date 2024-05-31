using Business;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HttpServer.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportController(ReportGenerator reportGenerator) : ControllerBase
{
	[HttpPost]
	public IActionResult Post(JsonElement jsonElement)
	{
		int id = jsonElement.GetProperty("id").GetInt32();
		string type = jsonElement.GetProperty("type").GetString();
		
		switch(type)
		{
			case "tour":
				reportGenerator.GenerateTourReport(id);
				break;
			case "summary":
				reportGenerator.GenerateSummaryReport(id);
				break;
			default: break;
		}
		
		return NoContent();
	}
}
