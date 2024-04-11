using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBanHangLapTop.Areas.Admin.Models;
using WebBanHangLapTop.EFRepository;
using WebBanHangLapTop.IRepository;
using WebBanHangLapTop.Models;
using WebBanHangLapTop.Repository;
namespace WebBanHangLapTop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class OrderController : Controller
    {
        private readonly IOrder _order;
        public OrderController(IOrder orderRepository)
        {
            _order = orderRepository;
            
        }
        public async Task<IActionResult> Index()
        {
            var order = await _order.GetAllAsync();
            ViewBag.categorys = await _order.GetAllAsync();
            return View(order);
        }
    }
}
