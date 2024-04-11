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
      

    }
}
