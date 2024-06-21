using WebAPIExample2.DTO;
using WebAPIExample2.Interfaces;
using WebAPIExample2.IServices;
using WebAPIExample2.Models;

namespace WebAPIExample2.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public async Task<Service> GetService(int serviceId)
        {
            return await _serviceRepository.GetService(serviceId);
        }
        public async Task<IEnumerable<Service>> GetServices()
        {
            return await _serviceRepository.GetServices();
        }
        public async Task AddService(ServiceDTO serviceDTO)
        {
            await _serviceRepository.AddService(serviceDTO);
        }
        public async Task UpdateService(ServiceDTO serviceDTO)
        {
            await _serviceRepository.UpdateService(serviceDTO);
        }
        public async Task DeleteService(int serviceId)
        {
            await _serviceRepository.DeleteService(serviceId);
        }
    }
}
