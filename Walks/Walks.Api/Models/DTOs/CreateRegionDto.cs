namespace Walks.Api.Models.DTOs
{
    public class CreateRegionDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
