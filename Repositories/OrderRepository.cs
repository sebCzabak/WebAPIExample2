using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebAPIExample2.Data;
using WebAPIExample2.DTO;
using WebAPIExample2.Interfaces;
using WebAPIExample2.Models;

namespace WebAPIExample2.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _dataContext;
        public OrderRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<OrderDetail> GetOrder(int orderId)
        {
            var order = await _dataContext.Order.FindAsync(orderId);
            if (order != null)
                return new OrderDetail
                {
                    OrderId = orderId,
                    ServicesIds = await _dataContext.ServiceOrder
                    .Where(so => so.OrderId == order.OrderId)
                    .Select(so => so.ServiceId)
                    .ToListAsync()
                };
            return null;
        }
        public async Task<IEnumerable<OrderDetail>> GetOrders()
        {
            var ordersDetails = await _dataContext.Order
                .Select(order => new OrderDetail
                {
                    OrderId = order.OrderId,
                    ServicesIds = _dataContext.ServiceOrder
                        .Where(so => so.OrderId == order.OrderId)
                        .Select(so => so.ServiceId)
                        .ToList()
                })
                .ToListAsync();

            return ordersDetails;
        }
        public async Task<bool> AddOrder(OrderDTO orderDTO)
        {

            var user = await _dataContext.User.FindAsync(orderDTO.UserId);
            int ordersSum = await _dataContext.Order.Select(o => o.OrderId).CountAsync();

            if (user != null)
            {
                var order = new Order
                {
                    UserId = orderDTO.UserId,
                    User = user,
                    ServiceName = orderDTO.ServiceName,
                    Description = orderDTO.Description,
                    Status = orderDTO.Status,
                    Date = orderDTO.Date
                };

                await _dataContext.Order.AddAsync(order);
                await _dataContext.SaveChangesAsync();

                var serviceOrders = new List<ServiceOrder>();
                foreach (var serviceId in orderDTO.ServicesIds)
                {
                    var service = await _dataContext.Service.FindAsync(serviceId);

                    if (service != null)
                    {
                        var orderService = new ServiceOrder
                        {
                            ServiceId = serviceId,
                            OrderId = order.OrderId,
                        };
                        serviceOrders.Add(orderService);
                    }
                    else
                    {
                        return false;
                    }
                }
                await _dataContext.ServiceOrder.AddRangeAsync(serviceOrders);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task UpdateOrder(OrderDTO orderDTO)
        {
            var order = await _dataContext.Order.FindAsync(orderDTO.OrderId);
            var user = await _dataContext.User.FindAsync(orderDTO.UserId);

            if (order != null || user != null)
            {
                order.ServiceName= orderDTO.ServiceName;
                order.Description= orderDTO.Description;
                order.Complaint = orderDTO.Complaint;
                order.UserId = orderDTO.UserId;
                order.Status =orderDTO.Status;
                order.User = user;

                var currentServiceOrders = await _dataContext.ServiceOrder
                    .Where(so => so.OrderId == orderDTO.OrderId)
                    .ToListAsync();

                var currentServiceIds = currentServiceOrders.Select(so => so.ServiceId).ToHashSet();
                var newServiceIds = orderDTO.ServicesIds.ToHashSet();

                var serviceOrdersToAdd = newServiceIds.Except(currentServiceIds)
                    .Select(serviceId => new ServiceOrder
                    {
                        ServiceId = serviceId,
                        OrderId = orderDTO.OrderId
                    })
                    .ToList();

                var serviceOrdersToDelete = currentServiceOrders
                    .Where(so => !newServiceIds.Contains(so.ServiceId))
                    .ToList();

                await _dataContext.ServiceOrder.AddRangeAsync(serviceOrdersToAdd);
                _dataContext.ServiceOrder.RemoveRange(serviceOrdersToDelete);

                await _dataContext.SaveChangesAsync();
            }
        }
        public async Task DeleteOrder(int orderId)
        {
            var order = await _dataContext.Order.FindAsync(orderId);

            if (order != null)
            {
                var serviceOrders = await _dataContext.ServiceOrder
                    .Where(so => so.OrderId == orderId)
                    .ToListAsync();

                _dataContext.ServiceOrder.RemoveRange(serviceOrders);
                _dataContext.Order.Remove(order);
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}
