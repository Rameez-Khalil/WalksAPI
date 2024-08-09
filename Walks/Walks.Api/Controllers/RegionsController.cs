using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Walks.Api.Data;
using Walks.Api.Models.Domain;
using Walks.Api.Models.DTOs;
using Walks.Api.Repositories;

namespace Walks.Api.Controllers
{
    [Route("api/[controller]")] //localhost:port/api/regions.
    [ApiController] //This controller validates it for the purpose of API.
    public class RegionsController : ControllerBase
    {
        private readonly WalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }


        //GET ALL REGIONS. 
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            //Get all regions.
            var regions = await regionRepository.GetAllRegionsAsync();  //Domain.

            //Map domain to DTOs.
            var regionsDto = new List<GetRegionsDto>();

            foreach (var region in regions)
            {
                regionsDto.Add(new GetRegionsDto
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                }); 
            }; 
            

            //Return the DTOs.
            return Ok(regionsDto); 
        }

        //Get single user.
        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetRegionById([FromRoute]Guid id)
        {
            //Domain
            var region = await dbContext.Regions.FindAsync(id);

            //Check and return.
            if (region != null)
            {
                var regionDto = new GetRegionsDto
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                };
                return Ok(region);
            }

            return NotFound(); 
        }

        //CREATE NEW REGION.
        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] CreateRegionDto request)
        {
            //map DTO to domain model.
            var region = new Region
            {
                Code = request.Code,
                RegionImageUrl = request.RegionImageUrl,
                Name = request.Name
            };

            //use domain model and save changes.
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();

            //map this domain object to its DTO and expose the DTO not the Domain object.
            var regionDto = new GetRegionsDto
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            }; 

            return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto); 
        }


        //UPDATE REGION.
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdatRegion([FromRoute] Guid id, [FromBody] UpdateRegionDto request)
        {
            //Get the region.
            var regionDomain = await dbContext.Regions.FindAsync(id);
            if (regionDomain != null)
            {
                regionDomain.Code = request.Code;
                regionDomain.Name = request.Name; 
                regionDomain.RegionImageUrl = request.RegionImageUrl;

                await dbContext.SaveChangesAsync();


                //map it back to the dto and return the 201 response.
                var regionUpdatedDto = new GetRegionsDto
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageUrl = regionDomain.RegionImageUrl
                };

                return Ok(regionUpdatedDto); 

            }

            return NotFound(); 
        }


        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            //Find the object with the provided id.
            var region = await dbContext.Regions.FindAsync(id);

            //if the region is not null, then delete it.
            if (region != null)
            {
                dbContext.Regions.Remove(region); 
                await dbContext.SaveChangesAsync();

                //Convert it into a DTO.
                var regionDto = new CreateRegionDto
                {
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl,
                    Code = region.Code
                };

                return Ok(region); 

            }

            return NotFound(); 
        }

    }
}
