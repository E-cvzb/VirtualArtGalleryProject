using System.ComponentModel.DataAnnotations;
using VirtualArtGallery.Bussiness.Operations.Artwork.Dtos;

namespace VirtualArtGallery.WepApi.Models
{
    public class UpdateArtworkRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public List<ArtworkExhibitionDto> ExhibitionIds { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
