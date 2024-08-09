using Walks.Api.Models.Domain;
using Walks.Api.Models.DTOs;

namespace Walks.Api.Repositories
{
    public interface IRegionRepository
    {
        public Task<List<Region>> GetAllRegionsAsync();
        public Task<Region> GetRegionByIdAsync(Guid id);
        public Task<Region> UpdateRegionAsync(Guid id, UpdateRegionDto request);
        public Task<Region> DeleteRegionAsync(Guid id);
        public Task<Region> CreateRegionAsync(CreateRegionDto request); 
    }
}
