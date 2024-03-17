using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebBanHangLapTop.Models;
using WebBanHangLapTop.Repository;

namespace WebBanHangLapTop.Controllers
{
	public class ProductController : Controller
	{

		//URL : Product/Add

		private readonly IProductRepository _productRepository;
		private readonly ICategoryRepository _categoryRepository;
		public ProductController(IProductRepository productRepository,
		ICategoryRepository categoryRepository)
		{
			_productRepository = productRepository;
			_categoryRepository = categoryRepository;
		}
		// Hiển thị danh sách sản phẩm
		public async Task<IActionResult> Index()
		{
			var product1 = await _productRepository.GetAllAsync();
			return View(product1);
		}
		// Hiển thị form thêm sản phẩm mới
		public async Task<IActionResult> AddAsync()
		{

			var categories = await _categoryRepository.GetAllAsync();
			ViewBag.Categories = new SelectList(categories, "Id", "Name");
			return View();
		}
		// Xử lý thêm sản phẩm mới
		[HttpPost]
		public async Task<IActionResult> Add(Product product,IFormFile ImageUrl)
		{
			string url = "";
			if (ModelState.IsValid)
			{
				if (ImageUrl != null)
				{
                    url = await SaveImage(ImageUrl);
				}
				await _productRepository.AddAsync(product, url);
				return RedirectToAction(nameof(Index));
			}
			// Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
			var categories = await _categoryRepository.GetAllAsync();
			ViewBag.Categories = new SelectList(categories, "Id", "Name");
			return View(product);
		}
		// Hiển thị thông tin chi tiết sản phẩm
		public async Task<IActionResult> Display(int id)
		{
			var product = await _productRepository.GetByIdAsync(id);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}
		// Hiển thị form cập nhật sản phẩm
		public async Task<IActionResult> Update(int id)
		{
			var product = await _productRepository.GetByIdAsync(id);
			if (product == null)
			{
				return NotFound();
			}
			var categories = await _categoryRepository.GetAllAsync();
			ViewBag.Categories = new SelectList(categories, "Id", "Name",

			product.CategoryId);

			return View(product);

		}
		// Xử lý cập nhật sản phẩm
		[HttpPost]
		public async Task<IActionResult> Update(int id, Product product,IFormFile ImageUrl)
		{
			
				if (ImageUrl != null)
				{
						product.ImageUrl=await SaveImage(ImageUrl);	
				}
				if (id != product.Id)
				{
					return NotFound();
				}
				if (ModelState.IsValid)
				{
					await _productRepository.UpdateAsync(product);
					return RedirectToAction(nameof(Index));
				
            }
            return View(product);
		}
		// Hiển thị form xác nhận xóa sản phẩm
		public async Task<IActionResult> Delete(int id)
		{
			var product = await _productRepository.GetByIdAsync(id);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}
		// Xử lý xóa sản phẩm
		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _productRepository.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}


		private async Task<string> SaveImage(IFormFile image)
		{
			var savePath = Path.Combine("wwwroot/Image", image.FileName);
			using(var fileStream = new FileStream(savePath, FileMode.Create))
			{
				await image.CopyToAsync(fileStream);
			}
			return "/Image/" + image.FileName;
		}

    }
}