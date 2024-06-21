using Microsoft.EntityFrameworkCore;
using WebAPIExample2.Data;
using WebAPIExample2.IServices;
using WebAPIExample2.Models;

public class PartService : IPartService
{
    private readonly DataContext _dataContext;

    public PartService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Part> GetPart(int partId)
    {
        return await _dataContext.Part.FindAsync(partId);
    }

    public async Task<IEnumerable<Part>> GetParts()
    {
        return await _dataContext.Part.ToListAsync();
    }

    public async Task<bool> AddPart(Part partModel)
    {
        if (await _dataContext.Part.AnyAsync(p => p.Name == partModel.Name))
        {
            return false;
        }

        await _dataContext.Part.AddAsync(partModel);
        await _dataContext.SaveChangesAsync();
        return true;
    }

    public async Task UpdatePart(Part partModel)
    {
        _dataContext.Part.Update(partModel);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeletePart(int partId)
    {
        var part = await _dataContext.Part.FindAsync(partId);
        if (part != null)
        {
            _dataContext.Part.Remove(part);
            await _dataContext.SaveChangesAsync();
        }
    }

    public async Task<bool> IssueParts(IssuePartsRequestModel request)
    {
        foreach (var partRequest in request.Parts)
        {
            var part = await _dataContext.Part.FindAsync(partRequest.PartId);
            if (part == null || part.Amount < partRequest.Quantity)
            {
                return false;
            }

            part.Amount -= partRequest.Quantity;
        }

        var partRequestEntity = await _dataContext.PartRequest.FindAsync(request.RequestId);
        if (partRequestEntity != null)
        {
            partRequestEntity.Status = "Części wydane";
        }

        await _dataContext.SaveChangesAsync();

        return true;
    }
}
