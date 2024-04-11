using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Drawing2D;
using WebBanHangLapTop.Areas.Admin.Models;
using WebBanHangLapTop.IRepository;
using WebBanHangLapTop.Models;
using WebBanHangLapTop.Repository;

namespace WebBanHangLapTop.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]
	public class AdminBrand : Controller
	{
		private readonly IBrand _brandRepository;
		public AdminBrand(IBrand brandRepository)
		{
			_brandRepository = brandRepository;
		}
		public async Task<IActionResult> Index()
		{
			var brand = await _brandRepository.GetAllAsync();
			return View(brand);
		}
		public async Task<IActionResult> Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(Brand brand)
		{
			if (ModelState.IsValid)
			{
				await _brandRepository.AddAsync(brand);
				return RedirectToAction("Index");
			}
			return View(brand);
		}
		public async Task<IActionResult> Edit(int id)
		{
			var brand = await _brandRepository.GetByIdAsync(id);

			if (brand == null)
			{
				return NotFound();
			}
			return View(brand);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(Brand brand)
		{
			if (ModelState.IsValid)
			{
				await _brandRepository.UpdateAsync(brand);
				return RedirectToAction(nameof(Index));
			}

			return View(brand);
		}
		public async Task<IActionResult> Delete(int id)
		{
			var brand = await _brandRepository.GetByIdAsync(id);
			if (brand == null)
			{
				return NotFound();
			}
			return View(brand);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _brandRepository.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
