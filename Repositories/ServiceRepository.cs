using Microsoft.EntityFrameworkCore;
using WebAPIExample2.Data;
using WebAPIExample2.DTO;
using WebAPIExample2.Interfaces;
using WebAPIExample2.Models;

namespace WebAPIExample2.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly DataContext _dataContext;
        public ServiceRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Service> GetService(int serviceId)
        {
            var service = await _dataContext.Service.FindAsync(serviceId);
            return service != null ? service : new Service();
        }
        public async Task<IEnumerable<Service>> GetServices()
        {
            return await _dataContext.Service.ToListAsync();
        }

        public async Task AddService(ServiceDTO serviceDTO)
        {
            var service = new Service
            {
                ServiceName = serviceDTO.ServiceName,
                Description = serviceDTO.Description,
                Price = serviceDTO.Price

            };
            await _dataContext.Service.AddAsync(service);
            await _dataContext.SaveChangesAsync();
        }
        public async Task UpdateService(ServiceDTO serviceDTO)
        {
            var service = await _dataContext.Service.FindAsync(serviceDTO.ServiceId);

            if (service != null)
            {
                service.ServiceName = serviceDTO.ServiceName;
                service.Description = serviceDTO.Description;
                service.Price = serviceDTO.Price;

                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task DeleteService(int serviceId)
        {
            var service = await _dataContext.Service.FindAsync(serviceId);

            if (service != null)
            {
                _dataContext.Service.Remove(service);
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}
