using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAPIExample2.Data;
using WebAPIExample2.DTO;
using WebAPIExample2.IServices;
using WebAPIExample2.Models;

namespace WebAPIExample2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly DataContext _dataContext;
        

        public OrderController(IOrderService orderService,DataContext dataContext)
        {
            _orderService = orderService;
            _dataContext = dataContext;
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult> GetOrder(int orderId)
        {
            var order = await _orderService.GetOrder(orderId);
            if (order != null)
            {
                return Ok(order);
            }
            return NotFound($"Lack of order with id: {orderId}");
        }

        [HttpGet]
        [Route("orders")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetOrders();
            if (orders != null && orders.Any())
            {
                return Ok(orders);
            }
            return NotFound("Lack of orders");
        }
        [HttpGet]
        [Route("mechanic-orders")]
        public async Task<List<OrderDTO>> GetMechanicsOrders()
        {
            var orders = await _dataContext.Order
                .Include(o => o.ServiceOrders)
                    .ThenInclude(so => so.Service)
                .Select(o => new OrderDTO
                {
                    OrderId = o.OrderId,
                    UserId = o.UserId,
                    ServiceName = o.ServiceOrders.FirstOrDefault().Service.ServiceName, // Ensure ServiceOrders are not empty
                    Status = o.Status
                })
                .ToListAsync();

            return orders;
        }
        [HttpPost]
        [Route("create")]
        public async Task<bool> AddOrder(OrderDTO orderDTO)
        {
            var user = await _dataContext.User.FindAsync(orderDTO.UserId);

            if (user != null)
            {
                var order = new Order
                {
                    UserId = orderDTO.UserId,
                    User = user,
                    ServiceName = orderDTO.ServiceName,
                    Description = orderDTO.Description,
                    Status = orderDTO.Status,
                    Date = orderDTO.Date,
                    //Price = orderDTO.Price
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
                            ServiceName = service.ServiceName 
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



        [HttpPut]
        public async Task<ActionResult> UpdateOrder(OrderDTO orderDTO)
        {
            var existingOrder = await _orderService.GetOrder(orderDTO.OrderId);
            if (existingOrder == null)
            {
                return NotFound("There is no order like this one");
            }

            await _orderService.UpdateOrder(orderDTO);
            return Ok("The part has been updated");
        }
        [HttpPut("orders/{orderId}/status")]
        public async Task<ActionResult> UpdateOrderStatus(int orderId, [FromBody]OrderStatusUpdateDTO statusUpdate)

        {
            Console.WriteLine($"Received request to update order {orderId} status to {statusUpdate.Status} by {statusUpdate.MechanicName}");
            var order = await _dataContext.Order.FindAsync(orderId);
            if (order == null)
            {
                Console.WriteLine($"Order {orderId} not found.");
                return NotFound();
            }

            order.Status = statusUpdate.Status;
            order.MechanicName = statusUpdate.MechanicName; 
            await _dataContext.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{orderId}")]
        public async Task<ActionResult> DeleteOrder(int orderId)
        {
            var existingOrder = await _orderService.GetOrder(orderId);
            if (existingOrder == null)
            {
                return NotFound();
            }

            await _orderService.DeleteOrder(orderId);
            return NoContent();
        }
        [HttpGet("part-requests")]
        public async Task<ActionResult<IEnumerable<PartRequestDTO>>> GetPartRequests()
        {
            var partRequests = await _dataContext.ServiceOrder
                .Include(so => so.Service)
                .Include(so => so.Order)
                .ThenInclude(o => o.User)
                .Select(so => new PartRequestDTO
                {
                    RequestId = so.OrderId,
                    MechanicName = so.Order.User.Email,
                    Parts = so.Service.ServiceName,
                    Status = so.Order.Status
                })
                .ToListAsync();

            return Ok(partRequests);
        }
    }
}
