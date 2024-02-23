using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Repositories
{
    public interface IRegionRepository
    {
        public Task<List<Region>> GetAll();
        public Task<Region?> GetById(Guid id);
        public Task<Region> Create(Region addRegionRequestDTO);
        public Task<Region?> Update(Guid id, Region region);
        public Task<Region?> Delete(Guid id);
    }
}
