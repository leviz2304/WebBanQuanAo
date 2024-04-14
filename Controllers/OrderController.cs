using Microsoft.AspNetCore.Mvc;
using WebBanHangLapTop.IRepository;
using WebBanHangLapTop.Repository;
using System.Security.Claims;
using WebBanHangLapTop.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization; // For getting user claims
namespace WebBanHangLapTop.Controllers
{
    public class OrderController : Controller
    {

        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrand _brandRepository;
        private readonly IOrder _orderRepository;


        public OrderController(IProductRepository productRepository,
        ICategoryRepository categoryRepository, IBrand brandRepository, IOrder orderRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _orderRepository = orderRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> DisplayOrder() {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // This gets the current logged-in user's ID
            var orders = await _orderRepository.GetOrdersByUserId(userId);
            return View(orders);
        }
    }
}
