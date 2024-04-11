using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBanHangLapTop.Models;
using WebBanHangLapTop.Repository;

namespace WebBanHangLapTop.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(IProductRepository productRepository,
        ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        // 
        [Authorize(Roles = "Admin")]
    
        public async Task<IActionResult> CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
               await _categoryRepository.AddAsync(category);
                return RedirectToAction("Category");
            }
            return View(category);
        }

        public async Task<IActionResult> Category()
        {
            ViewBag.ListNumberCategori = await _productRepository.GetListCountCategory();
            var category = await _categoryRepository.GetAllAsync();
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
    }
}
