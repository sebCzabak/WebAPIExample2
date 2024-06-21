using Microsoft.EntityFrameworkCore;
using WebAPIExample2.Data;
using WebAPIExample2.Interfaces;
using WebAPIExample2.IRepositories;
using WebAPIExample2.Models;

namespace WebAPIExample2.Repositories
{
    public class PartRepository : IPartRepository
    {
        private readonly DataContext _dataContext;
        public PartRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Part> GetPart(int partId)
        {
            var part = await _dataContext.Part.FindAsync(partId);
            return part != null ? part : null;
        }

        public async Task<IEnumerable<Part>> GetParts()
        {
            return await _dataContext.Part.ToListAsync();
        }

        public async Task<bool> AddPart(Part partModel)
        {
            var part = await _dataContext.Part.FirstOrDefaultAsync(p => p.Name == partModel.Name);
            if (part != null)
            {
                return false;
            }
            await _dataContext.AddAsync(partModel);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task UpdatePart(Part partModel)
        {
            var part = await _dataContext.Part.FindAsync(partModel.PartId);

            if (part != null)
            {
                part.Name = partModel.Name;
                part.Amount = partModel.Amount;

                await _dataContext.SaveChangesAsync();
            }
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
    }
}
