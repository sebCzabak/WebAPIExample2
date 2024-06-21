﻿using WebAPIExample2.Models;

namespace WebAPIExample2.IServices
{
    public interface IPartService
    {
        public Task<Part> GetPart(int partId);
        public Task<IEnumerable<Part>> GetParts();
        public Task<bool> AddPart(Part partModel);
        public Task UpdatePart(Part partModel);
        public Task DeletePart(int partId);
        Task<bool> IssueParts(IssuePartsRequestModel request);

    }
}
