using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HttpServer.Controllers;

[ApiController]
[Route("[controller]")]
public class TourController(IService<Tour> tourService) : ControllerBase
{
	[HttpGet]
	public IEnumerable<Tour>? Get()
	{
		return tourService.GetAll();
	}

	[HttpGet("{id}")]
	public ActionResult<Tour> Get(int id)
	{
		Tour? tour = tourService.GetById(id);
		return tour is null ? (ActionResult<Tour>)NotFound() : (ActionResult<Tour>)tour;
	}

	//[HttpPost]
	//public ActionResult<Tour> Post(string tourJson)
	//{
	//	Tour tour = JsonSerializer.Deserialize<Tour>(tourJson);

	//	return (ActionResult<Tour>)tourService.Create(tour);
	//}

	[HttpPost]
	public ActionResult<Tour> Post(Tour tour)
	{
		return (ActionResult<Tour>)tourService.Create(tour);
	}

	[HttpPut]
	public ActionResult<Tour> Put(Tour updatedTour)
	{
		return (ActionResult<Tour>)tourService.Update(updatedTour);
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		tourService.Delete(id);
		return NoContent();
	}
}