using UI.Interfaces;
using UI.Models;

namespace UI.Managers;
public class TourManager(IHttpHelper<TourModel> tourHelper) : IManager<TourModel>
{
	public async Task<TourModel?> CreateAsync(TourModel entity)
	{
		return await tourHelper.CreateDataAsync(entity);
	}

	public void DeleteAsync(TourModel entity)
	{
		throw new NotImplementedException();
	}

	public Task<List<TourModel>?> GetAllAsync()
	{
		throw new NotImplementedException();
	}

	public Task<TourModel?> GetByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<TourModel?> UpdateAsync(TourModel entity)
	{
		throw new NotImplementedException();
	}
}
