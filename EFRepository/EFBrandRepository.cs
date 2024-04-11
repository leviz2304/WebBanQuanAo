﻿using Microsoft.EntityFrameworkCore;
using WebBanHangLapTop.IRepository;
using WebBanHangLapTop.Models;

namespace WebBanHangLapTop.EFRepository
{
	public class EFBrandRepository:IBrand
	{
		private readonly ApplicationDbContext _context;
		public EFBrandRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		// Tương tự như EFProductRepository, nhưng cho Category
		public async Task<IEnumerable<Brand>> GetAllAsync()
		{
			return await _context.Brands.ToListAsync();
		}
		public async Task<Brand> GetByIdAsync(int id)
		{
			return await _context.Brands.FindAsync(id);
		}
		public async Task AddAsync(Brand brand)
		{
			_context.Brands.Add(brand);
			await _context.SaveChangesAsync();
		}
		public async Task UpdateAsync(Brand brand)
		{
			_context.Brands.Update(brand);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteAsync(int id)
		{
			var brand = await _context.Brands.FindAsync(id);
			_context.Brands.Remove(brand);
			await _context.SaveChangesAsync();
		}
	}
}
