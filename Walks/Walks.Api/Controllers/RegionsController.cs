using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Walks.Api.Data;
using Walks.Api.Models.Domain;
using Walks.Api.Models.DTOs;

namespace Walks.Api.Controllers
{
    [Route("api/[controller]")] //localhost:port/api/regions.
    [ApiController] //This controller validates it for the purpose of API.
    public class RegionsController : ControllerBase
    {
        private readonly WalksDbContext dbContext;

        public RegionsController(WalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        //GET ALL REGIONS. 
        [HttpGet]
        public IActionResult GetAllRegions()
        {
            //Get all regions.
            var regions = dbContext.Regions.ToList(); //Domain.

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

        public IActionResult GetRegionById([FromRoute]Guid id)
        {
            //Domain
            var region = dbContext.Regions.Find(id);

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
        public IActionResult CreateRegion([FromBody] CreateRegionDto request)
        {
            //map DTO to domain model.
            var region = new Region
            {
                Code = request.Code,
                RegionImageUrl = request.RegionImageUrl,
                Name = request.Name
            };

            //use domain model and save changes.
            dbContext.Regions.Add(region);
            dbContext.SaveChanges();

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

    }
}
