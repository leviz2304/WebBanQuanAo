using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebBanHangLapTop.Models;
using WebBanHangLapTop.Repository;

namespace WebBanHangLapTop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;


        public HomeController(ILogger<HomeController> logger,IProductRepository productRepository   )
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products=await _productRepository.GetAllAsync();
            return View(products);
        }
        public async Task<IActionResult> Display(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.images = await _productRepository.GetAllImagesById(id);

            return View(product);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
