using AutoMapper;
using Walks.Api.Models.Domain;
using Walks.Api.Models.DTOs;

namespace Walks.Api.Mapping
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Region, GetRegionsDto> ().ReverseMap();
            CreateMap<CreateRegionDto, Region>().ReverseMap();
            CreateMap<Region, UpdateRegionDto> ().ReverseMap();
        }
    }
}
