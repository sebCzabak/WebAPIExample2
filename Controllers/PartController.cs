using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIExample2.Data;
using WebAPIExample2.IServices;
using WebAPIExample2.Models;
using WebAPIExample2.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebAPIExample2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartController : ControllerBase
    {
        private readonly IPartService _partService;
        private readonly DataContext _dataContext;

        public PartController(IPartService partService, DataContext dataContext)
        {
            _partService = partService;
            _dataContext = dataContext;
        }

        [HttpGet("{partId}")]
        public async Task<ActionResult> GetPart(int partId)
        {
            var part = await _partService.GetPart(partId);
            if (part != null)
            {
                return Ok(part);
            }
            return BadRequest($"Lack of part with id: {partId}");
        }

        [HttpGet]
        [Route("parts")]
        public async Task<IActionResult> GetParts()
        {
            var parts = await _partService.GetParts();
            if (parts != null)
            {
                return Ok(parts);
            }
            return NotFound("Lack of parts");
        }

        [HttpPost]
        public async Task<ActionResult> AddPart(Part partModel)
        {
            if (await _partService.AddPart(partModel))
            {
                return Ok(partModel);
            }
            return NotFound("Part of this name already exists");
        }

        [HttpPut("{partId}")]
        public async Task<ActionResult> UpdatePart(Part partModel)
        {
            var existingPart = await _partService.GetPart(partModel.PartId);
            if (existingPart == null)
            {
                return NotFound("There is no part like this one");
            }

            await _partService.UpdatePart(partModel);
            return Ok("The part has been updated");
        }

        [HttpDelete("{partId}")]
        public async Task<ActionResult> DeletePart(int partId)
        {
            var existingPart = await _partService.GetPart(partId);
            if (existingPart == null)
            {
                return NotFound();
            }

            await _partService.DeletePart(partId);
            return NoContent();
        }

        [HttpPut]
        [Route("issue-parts")]
        public async Task<IActionResult> IssueParts(IssuePartsRequestModel request)
        {
            foreach (var part in request.Parts)
            {
                var partEntity = await _dataContext.Part.FindAsync(part.PartId);
                if (partEntity == null || partEntity.Amount < part.Quantity)
                {
                    return BadRequest("Insufficient parts available.");
                }

                partEntity.Amount -= part.Quantity;
            }

            var partRequest = await _dataContext.PartRequest.FindAsync(request.RequestId);
            if (partRequest != null)
            {
                partRequest.Status = "Części wydane";
            }

            await _dataContext.SaveChangesAsync();

            return Ok();
        }
        [HttpPost("request-parts")]
        public async Task<IActionResult> RequestParts(PartRequest request)
        {
            var order = await _dataContext.Order.FindAsync(request.PartRequestId);
            if (order == null)
            {
                return BadRequest("Invalid order ID.");
            }

            var partRequest = new PartRequest
            {
                MechanicName = request.MechanicName,
                Parts = request.Parts,
                Status = "Pending",
            };

            await _dataContext.PartRequest.AddAsync(partRequest);
            await _dataContext.SaveChangesAsync();

            return Ok(partRequest);
        }


    }
}
