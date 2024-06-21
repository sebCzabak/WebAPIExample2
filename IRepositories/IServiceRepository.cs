using WebAPIExample2.DTO;
using WebAPIExample2.Models;

namespace WebAPIExample2.Interfaces
{
    public interface IServiceRepository
    {
        public Task<Service> GetService(int serviceId);
        public Task<IEnumerable<Service>> GetServices();
        public Task AddService(ServiceDTO serviceDTO);
        public Task UpdateService(ServiceDTO serviceDTO);
        public Task DeleteService(int serviceId);
    }
}
