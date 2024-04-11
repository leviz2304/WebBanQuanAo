using WebBanHangLapTop.Models;

namespace WebBanHangLapTop.IRepository
{
	public interface IBrand
	{

		Task<IEnumerable<Brand>> GetAllAsync();
		Task<Brand> GetByIdAsync(int id);
		Task AddAsync(Brand brand);
		Task UpdateAsync(Brand brand);
		Task DeleteAsync(int id);
	}
}
