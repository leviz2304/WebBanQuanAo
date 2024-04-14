
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebBanHangLapTop.Areas.Admin.Models;
using WebBanHangLapTop.Models;
using WebBanHangLapTop.Repository;

namespace WebBanHangLapTop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger,IProductRepository productRepository, ApplicationDbContext context)
        {
            _logger = logger;
            _productRepository = productRepository;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            
            var topPicks = await _productRepository.GetTopPicksAsync(4); 
            return View(topPicks);
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

        //[HttpGet]
        //public IActionResult ResetPassword(string token, string email)
        //{
        //    var model = new ResetPasswordViewModel { Token = token, Email = email };
        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var user = await _userManager.FindByEmailAsync(model.Email);

        //    if (user != null)
        //    {
        //        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Login");
        //        }
        //    }
        //    return View(model);
        //}


    }
}
