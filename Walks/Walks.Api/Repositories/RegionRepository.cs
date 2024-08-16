using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using Walks.Api.Data;
using Walks.Api.Models.Domain;
using Walks.Api.Models.DTOs;

namespace Walks.Api.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly WalksDbContext dbContext;
        public RegionRepository(WalksDbContext dbContext)
        {
            this.dbContext  = dbContext;
        }


        //GET ALL REGIONS. 
        public async Task<List<Region>> GetAllRegionsAsync()
        {
            return await dbContext.Regions.ToListAsync(); 
        }


        //GET REGIONS BY ID.
        public async Task<Region> GetRegionByIdAsync(Guid id)
        {
            //Find the region.
            var region = await dbContext.Regions.FindAsync(id);
            if (region != null)
            {
                return region; 
            }

            return null; 
        }

        //UPDATE REGION.
        public async Task<Region> UpdateRegionAsync(Guid id, UpdateRegionDto request)
        {
            //Find the region.
            var region = await dbContext.Regions.FindAsync(id);

            //Check if the region is not null.
            if (region != null) 
            { 
                region.Code = request.Code;
                region.Name = request.Name;
                region.RegionImageUrl = request.RegionImageUrl;

                await dbContext.SaveChangesAsync();
                return region;
            }

            return null; 
        }

        //CREATE REGION.
        public async Task<Region> CreateRegionAsync(Region request)
        {
            await dbContext.Regions.AddAsync(request);
            await dbContext.SaveChangesAsync(); 

            return request;
        }

        //DELETE REGION.
        public async Task<Region> DeleteRegionAsync(Guid id)
        {
            //Find the region.
            var region = await dbContext.Regions.FindAsync(id);

            if (region != null)
            {
                dbContext.Regions.Remove(region);
                await dbContext.SaveChangesAsync();
                return region;
            }

            return null;
        }
    }
}
