using Microsoft.EntityFrameworkCore;
using WebBanHangLapTop.IRepository;
using WebBanHangLapTop.Models;

namespace WebBanHangLapTop.EFRepository
{
    public class EFOrder:IOrder
    {
        private readonly ApplicationDbContext _context;
        public EFOrder(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }
       public async Task<IEnumerable<Order>> GetOrdersByUserId(string userId)
        {
            return await _context.Orders
                         .Where(o => o.UserId == userId)
                         .Include(o => o.OrderDetails)
                         .ThenInclude(od => od.Product)
                         .ThenInclude(p => p.Images) // Assuming the Product has a collection of Images
                         .ToListAsync();
        }


    }
}
