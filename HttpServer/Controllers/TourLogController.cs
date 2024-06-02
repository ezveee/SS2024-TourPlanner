using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace HttpServer.Controllers;

[ApiController]
[Route("[controller]")]
public class TourLogController(IService<TourLog> tourLogService) : ControllerBase
{
	[HttpGet]
	public IEnumerable<TourLog>? Get()
	{
		return tourLogService.GetAll();
	}

	[HttpGet("{id}")]
	public IEnumerable<TourLog>? Get(int id)
	{
		return tourLogService.GetLogsByTourId(id);
	}

	[HttpGet("logs/{id}")]
	public ActionResult<TourLog?> GetLog(int id)
	{
		return tourLogService.GetById(id);
	}

	[HttpPost]
	public ActionResult<TourLog> Post(TourLog tourLog)
	{
		return (ActionResult<TourLog>)tourLogService.Create(tourLog);
	}

	[HttpPut]
	public ActionResult<TourLog> Put(TourLog updatedTourLog)
	{
		return (ActionResult<TourLog>)tourLogService.Update(updatedTourLog);
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		tourLogService.Delete(id);
		return NoContent();
	}
}