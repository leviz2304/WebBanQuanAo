﻿using Microsoft.EntityFrameworkCore;
using WebBanHangLapTop.IRepository;
using WebBanHangLapTop.Models;

namespace WebBanHangLapTop.EFRepository
{
    public class EFOrderDetailsRepository : IOrderDetails
    {
        private readonly ApplicationDbContext _context;
        public EFOrderDetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDetail>> GetAllAsync()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        public async Task<IEnumerable<OrderDetail>> GetAllByOrderIDAsync(int orderId)
        {
            return await _context.OrderDetails.Where(p=>p.OrderId == orderId).ToListAsync();
        }
        //public async Task<IEnumerable<OrderDetail>> GetAllByOrderIDAsync(int orderId)
        //{
        //    return await _context.OrderDetails.FirstOrDefaultAsync(p => p.OrderId == orderId);
        //}
    }
}
