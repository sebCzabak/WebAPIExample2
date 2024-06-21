using WebAPIExample2.DTO;
using WebAPIExample2.Interfaces;
using WebAPIExample2.IServices;
using WebAPIExample2.Models;

namespace WebAPIExample2.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<OrderDetail> GetOrder(int orderId)
        {
            return await _orderRepository.GetOrder(orderId);
        }
        public async Task<IEnumerable<OrderDetail>> GetOrders()
        {
            return await _orderRepository.GetOrders();
        }
        public async Task<bool> AddOrder(OrderDTO orderDTO)
        {
            return await _orderRepository.AddOrder(orderDTO);
        }
        public async Task UpdateOrder(OrderDTO orderDTO)
        {
            await _orderRepository.UpdateOrder(orderDTO);
        }
        public async Task DeleteOrder(int orderId)
        {
            await _orderRepository.DeleteOrder(orderId);
        }
    }
}
