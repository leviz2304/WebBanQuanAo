using WebBanHangLapTop.Models;

namespace WebBanHangLapTop.IRepository
{
    public interface IOrder
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<IEnumerable<Order>>GetOrdersByUserId(string userId);
    }
}
