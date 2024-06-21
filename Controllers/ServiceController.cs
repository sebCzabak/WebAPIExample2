using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIExample2.DTO;
using WebAPIExample2.IServices;
using WebAPIExample2.Models;

namespace WebAPIExample2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet("{serviceId}")]
        public async Task<ActionResult> GetService(int serviceId)
        {
            var service = await _serviceService.GetService(serviceId);
            if (service != null)
            {
                return Ok(service);
            }
            return NotFound($"Lack of service with id: {serviceId}");
        }

        [HttpGet]
        [Route("Services")]
        public async Task<IActionResult> GetServices()
        {
            var services = await _serviceService.GetServices();
            if (services != null && services.Any())
            {
                return Ok(services);
            }
            return NotFound("Lack of services");
        }


        [HttpPost]
        public async Task<ActionResult> AddService(ServiceDTO serviceDTO)
        {
            await _serviceService.AddService(serviceDTO);
            return Ok(serviceDTO);
        }

        [HttpPut("{serviceId}")]
        public async Task<ActionResult> UpdateService(ServiceDTO serviceDTO)
        {
            var existingService = await _serviceService.GetService(serviceDTO.ServiceId);
            if (existingService == null)
            {
                return NotFound();
            }

            await _serviceService.UpdateService(serviceDTO);
            return NoContent();
        }

        [HttpDelete("{serviceId}")]
        public async Task<ActionResult> DeleteService(int serviceId)
        {
            var existingService = await _serviceService.GetService(serviceId);
            if (existingService == null)
            {
                return NotFound();
            }

            await _serviceService.DeleteService(serviceId);
            return NoContent();
        }
    }
}
