using WebBanHangLapTop.Models;

namespace WebBanHangLapTop.IRepository
{
    public interface IOrderDetails
    {
        Task<IEnumerable<OrderDetail>> GetAllByOrderIDAsync(int orderId);

        Task<IEnumerable<OrderDetail>> GetAllAsync();
    }
}
