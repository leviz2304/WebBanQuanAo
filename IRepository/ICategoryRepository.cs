﻿using WebBanHangLapTop.Models;

namespace WebBanHangLapTop.Repository
{
    public interface ICategoryRepository
    {
		//IEnumerable<Category> GetAllCategories();

		Task<IEnumerable<Category>> GetAllAsync();
		Task<Category> GetByIdAsync(int id);
		Task AddAsync(Category category);
		Task UpdateAsync(Category category);
		Task DeleteAsync(int id);

	
	}
}
