    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
using WebBanHangLapTop.Areas.Admin.Models;
using WebBanHangLapTop.IRepository;
using WebBanHangLapTop.Models;
    using WebBanHangLapTop.Repository;

    namespace WebBanHangLapTop.Areas.Admin.Controllers
    {
        [Area("Admin")]
        [Authorize(Roles =SD.Role_Admin)]
        public class AdminProduct : Controller
        {

            private readonly IProductRepository _productRepository;
            private readonly ICategoryRepository _categoryRepository;
        private readonly IBrand _brandRepository;

        public AdminProduct(IProductRepository productRepository,ICategoryRepository categoryRepository, IBrand brandRepository)
            {
                _productRepository = productRepository;
                _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;

        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
            {
                var product1 = await _productRepository.GetAllAsync();
                ViewBag.categorys = await _categoryRepository.GetAllAsync();
            var brand = await _brandRepository.GetAllAsync();
            ViewBag.Brands = await _brandRepository.GetAllAsync();
            return View(product1);
            }

            // GET: Admin/Products/Details/5
            public async Task<IActionResult> Details(int id)
            {
                var product = await _productRepository.GetByIdAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                ViewBag.images = await _productRepository.GetAllImagesById(id);

                return View(product);
            }

		// GET: Admin/Products/Create
		//          public async Task<IActionResult> Create()
		//          {
		//	    var categories = await _categoryRepository.GetAllAsync();
		//	    ViewBag.Categories = new SelectList(categories, "Id", "Name");
		//	    return View();
		//}
		public async Task<IActionResult> Add()
		{
			var category = await _categoryRepository.GetAllAsync();
			ViewBag.Categories = new SelectList(category, "Id", "Name");
            var brands = await _brandRepository.GetAllAsync();
            ViewBag.Brands = new SelectList(brands, "Id", "BrandName");
            return View();
		}
		    [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Add(Product product, IFormFile ImageUrl, List<IFormFile> Images)
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
                var categories = await _categoryRepository.GetAllAsync();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
            var brands = await _brandRepository.GetAllAsync();
            ViewBag.brands = new SelectList(brands, "Id", "BrandName");
            return View(product);
            }

            // GET: Admin/Products/Edit/5
            public async Task<IActionResult> Edit(int id)
            {
                var product = await _productRepository.GetByIdAsync(id);

                if (product == null)
                {
                    return NotFound();
                }
			    var categories = await _categoryRepository.GetAllAsync();
			    ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);
			    ViewBag.images = await _productRepository.GetAllImagesById(id);
                var brands = await _brandRepository.GetAllAsync();
                ViewBag.Brands = new SelectList(brands, "Id", "BrandName", product);
            return View(product);
            }

        
            [HttpPost]
            public async Task<IActionResult> Edit(int id, Product product, IFormFile? ImageUrl)
            {
			ModelState.Remove("ImageUrl"); // Loại bỏ xác thực ModelState cho
			
if (id != product.Id)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				var existingProduct = await
				_productRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync
													 // Giữ nguyên thông tin hình ảnh nếu không có hình mới được
			if (ImageUrl == null)
				{
					product.ImageUrl = existingProduct.ImageUrl;
				}
				else
				{
					// Lưu hình ảnh mới
					product.ImageUrl = await SaveImage(ImageUrl);
				}
				existingProduct.Name = product.Name;
				existingProduct.Price = product.Price;
				existingProduct.Description = product.Description;
				existingProduct.CategoryId = product.CategoryId;
				existingProduct.ImageUrl = product.ImageUrl;
				await _productRepository.UpdateAsync(existingProduct);
				return RedirectToAction(nameof(Index));
			}
			var categories = await _categoryRepository.GetAllAsync();
			ViewBag.Categories = new SelectList(categories, "Id", "Name");
			return View(product);
		}

            // GET: Admin/Products/Delete/5
            public async Task<IActionResult> Delete(int id)
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }

            // POST: Admin/Products/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                await _productRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }

            private async Task<string> SaveImage(IFormFile image)
            {
                var savePath = Path.Combine("wwwroot/Image", image.FileName);
                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
                return "/Image/" + image.FileName;
            }
        }
    }
