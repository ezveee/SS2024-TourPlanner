using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace HttpServer.Controllers;

[ApiController]
[Route("[controller]")]
public class TourController(IService<Tour> tourService) : ControllerBase
{
	private readonly IService<Tour> _tourService = tourService;
	private static readonly List<Tour> Tours = [];

	[HttpGet]
	public IEnumerable<Tour>? Get()
	{
		return _tourService.GetAll();
	}

	[HttpGet("{id}")]
	public ActionResult<Tour> Get(int id)
	{
		Tour tour = Tours.Find(t => t.TourId == id);
		return tour == null ? (ActionResult<Tour>)NotFound() : (ActionResult<Tour>)tour;
	}

	[HttpPost]
	public ActionResult<Tour> Post(Tour tour)
	{
		tour.TourId = Tours.Count + 1;
		Tours.Add(tour);
		return CreatedAtAction(nameof(Get), new { id = tour.TourId }, tour);
	}

	[HttpPut("{id}")]
	public IActionResult Put(int id, Tour updatedTour)
	{
		Tour tour = Tours.Find(t => t.TourId == id);
		if (tour == null)
		{
			return NotFound();
		}

		tour.Name = updatedTour.Name;
		tour.Description = updatedTour.Description;
		return NoContent();
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		Tour tour = Tours.Find(t => t.TourId == id);
		if (tour == null)
		{
			return NotFound();
		}

		_ = Tours.Remove(tour);
		return NoContent();
	}
}