using AutoMapper;
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
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        //GET ALL REGIONS. 
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            //Get all regions.
            var regions = await regionRepository.GetAllRegionsAsync();  //Domain.

                        
            //Return the DTOs.
            return Ok(mapper.Map<List<Region>>(regions)); 
        }

        //Get single Region.
        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetRegionById([FromRoute]Guid id)
        {
            //Domain
            var region = await regionRepository.GetRegionByIdAsync(id);

            //Check and return.
            if (region != null)
            {
           
                return Ok(mapper.Map<GetRegionsDto>(region));
            }

            return NotFound(); 
        }

        //CREATE NEW REGION.
        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] CreateRegionDto request)
        {
            //map DTO to domain model.
            var region = mapper.Map<Region>(request);

            var createdRegion = await regionRepository.CreateRegionAsync(region); 

           
            return CreatedAtAction(nameof(GetRegionById), new { id = createdRegion.Id }, createdRegion); 
        }


        //UPDATE REGION.
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdatRegion([FromRoute] Guid id, [FromBody] UpdateRegionDto request)
        {
            //Get the region.
            var regionDomain = await regionRepository.UpdateRegionAsync(id, request);
            if (regionDomain != null)
            {

                //map it back to the dto and return the 201 response.
                
                return Ok(mapper.Map<UpdateRegionDto>(regionDomain)); 

            }

            return NotFound(); 
        }


        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            //Find the object with the provided id.
            var region = await regionRepository.DeleteRegionAsync(id);

            //if the region is not null, then delete it.
            if (region != null)
            {
 
                //Convert it into a DTO.
                

                return Ok(mapper.Map<CreateRegionDto>(region)); 

            }

            return NotFound(); 
        }

    }
}
