﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebBanHangLapTop.Extension;
using WebBanHangLapTop.Models;
using WebBanHangLapTop.Repository;

namespace WebBanHangLapTop.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ShoppingCartController(ApplicationDbContext context,
UserManager<ApplicationUser> userManager, IProductRepository
productRepository)
        {
            _productRepository = productRepository;
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            ViewBag.Cart = cart.Items;
            return View(new Order());
        }

        
        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            var cart =HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart == null || !cart.Items.Any())
            {
                // Xử lý giỏ hàng trống...
                return RedirectToAction("Index");
            }
            var user = await _userManager.GetUserAsync(User);
            order.UserId = user.Id;
            order.OrderDate = DateTime.UtcNow;
            order.TotalPrice = cart.Items.Sum(i => i.Price * i.Quantity);
            order.OrderDetails = cart.Items.Select(i => new OrderDetail
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList();
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            HttpContext.Session.Remove("Cart");
            return View("OrderCompleted", order.Id);
        }
        private async Task<IActionResult> UpdateQuantity(int productId,int quantity)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");

            if (cart != null)
            {
                var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
                if (cartItem != null)
                {
                    cartItem.Quantity = Math.Max(quantity, 0);

                    if (cartItem.Quantity == 0)
                    {
                        cart.Items.Remove(cartItem);
                    }
                    HttpContext.Session.SetObjectAsJson("Cart", cart);
                }
            }
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            // Giả sử bạn có phương thức lấy thông tin sản phẩm từ productId
            var product = await GetProductFromDatabase(productId);
            var cartItem = new CartItem
            {
                ProductId = productId,
                Name = product.Name,
                ImageUrl= product.ImageUrl,
                Price = product.Price,
                Quantity = quantity
            };
            var cart =  HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new
            ShoppingCart();
            cart.AddItem(cartItem);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            return View(cart);
        }
        private async Task<Product> GetProductFromDatabase(int productId)
        {
            // Truy vấn cơ sở dữ liệu để lấy thông tin sản phẩm
            var product = await _productRepository.GetByIdAsync(productId);
            return product;
        }
        public IActionResult RemoveFromCart(int productId)
        {
            var cart =
            HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart is not null)
            {
                cart.RemoveItem(productId);
                // Lưu lại giỏ hàng vào Session sau khi đã xóa mục
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

    }
}
