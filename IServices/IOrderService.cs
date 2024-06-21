using WebAPIExample2.DTO;
using WebAPIExample2.Models;

namespace WebAPIExample2.IServices
{
    public interface IOrderService
    {
        public Task<OrderDetail> GetOrder(int orderId);
        public Task<IEnumerable<OrderDetail>> GetOrders();
        public Task<bool> AddOrder(OrderDTO orderDTO);
        public Task UpdateOrder(OrderDTO orderDTO);
        public Task DeleteOrder(int orderId);
    }
}
