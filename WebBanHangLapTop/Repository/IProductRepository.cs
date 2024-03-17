using WebBanHangLapTop.Models;

namespace WebBanHangLapTop.Repository
{
    public interface IProductRepository //khong co code o than ham
    {
		/*IEnumerable<Product> GetAll(); //danh sach product 
        Product GetById(int id); //1 product
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);*/

		Task<IEnumerable<Product>> GetAllAsync();
		Task<Product> GetByIdAsync(int id);
		Task AddAsync(Product product, string url);
		Task UpdateAsync(Product product);
		Task DeleteAsync(int id);
	}
}
