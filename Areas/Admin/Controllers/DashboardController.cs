using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBanHangLapTop.Areas.Admin.Models;
using WebBanHangLapTop.IRepository;
using WebBanHangLapTop.Models;

namespace WebBanHangLapTop.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IOrder _order;

        public DashboardController(IOrder order)
        {
            _order = order;
        }


		[Area("Admin")]
		[Authorize(Roles = SD.Role_Admin)]
		public async Task<IActionResult> Index()
        {
            return View("Index");
        }
    }
}
