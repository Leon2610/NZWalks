using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class AddRegionRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage ="Code has to be a minimun of 3 characteres")]
        [MaxLength(3, ErrorMessage = "Code has to be a maximum of 3 characteres")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characteres")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
