using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebBanHangLapTop.Areas.Admin.Models;
using WebBanHangLapTop.IRepository;
using WebBanHangLapTop.Models;
using WebBanHangLapTop.Repository;

namespace WebBanHangLapTop.Controllers
{
	
	public class ProductController : Controller
	{

		//URL : Product/Add

		private readonly IProductRepository _productRepository;
		private readonly ICategoryRepository _categoryRepository;
        private readonly IBrand _brandRepository;

        public ProductController(IProductRepository productRepository,
		ICategoryRepository categoryRepository,IBrand brandRepository)
		{
			_productRepository = productRepository;
			_categoryRepository = categoryRepository;
            _brandRepository= brandRepository;

        }
		// Hiển thị danh sách sản phẩm

		public IActionResult Layout2()
		{
			return View();	
		}
		public async Task< IActionResult> Index(string searchQuery, int? categoryId, int? brandId)
		{
            IEnumerable<Product> products;

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                products = await _productRepository.SearchAsync(searchQuery);
            }
            else
            {
                products = await _productRepository.GetFilteredAsync(categoryId, brandId);
            }
            var categories = await _categoryRepository.GetAllAsync();
            var brands = await _brandRepository.GetAllAsync();

            var viewModel = new HomePageViewModel
            {
                Products = products,
                Categories = categories,
                Brands = brands,
                SearchQuery = searchQuery,
                SelectedCategoryId = categoryId,
                SelectedBrandId = brandId
            };

            return View(viewModel);
        }


    // Hiển thị form thêm sản phẩm mới
    [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> AddAsync()
		{

			var categories = await _categoryRepository.GetAllAsync();
			ViewBag.Categories = new SelectList(categories, "Id", "Name");
            var brands = await _brandRepository.GetAllAsync();
            ViewBag.Brands = new SelectList(brands, "Id", "BrandName");
            return View();
		}
		// Xử lý thêm sản phẩm mới
		[HttpPost]
		public async Task<IActionResult> Add(Product product,IFormFile ImageUrl, List<IFormFile> Images)
		{
			string url = "";
			if (ModelState.IsValid)
			{
				if (ImageUrl != null)
				{
                   product.ImageUrl = await SaveImage(ImageUrl);
				}


    

				await _productRepository.AddAsync(product, Images);

				return RedirectToAction(nameof(Index));


			}
			// Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
			var categories = await _categoryRepository.GetAllAsync();
			ViewBag.Categories = new SelectList(categories, "Id", "Name");
            var brands = await _brandRepository.GetAllAsync();
            ViewBag.brands = new SelectList(brands, "Id", "BrandName");
            return View(product);
		}
		public async Task<IActionResult>Display(int id)
		{
			var product = await _productRepository.GetByIdAsync(id);
		
			if (product == null)
			{
				return NotFound();
			}

			ViewBag.images = await _productRepository.GetAllImagesById(id);

			return View(product);
		}
        // Hiển thị form cập nhật sản phẩm
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id)
		{
			var product = await _productRepository.GetByIdAsync(id);
			if (product == null)
			{
				return NotFound();
			}
			var categories = await _categoryRepository.GetAllAsync();
			ViewBag.Categories = new SelectList(categories, "Id", "Name",product.CategoryId);
            var brands = await _brandRepository.GetAllAsync();
            ViewBag.Brands = new SelectList(brands, "Id", "BrandName", product);
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
        [Authorize(Roles = "Admin")]
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
		[HttpPost]
		public async Task<IActionResult> DeleteProduct(int id)
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