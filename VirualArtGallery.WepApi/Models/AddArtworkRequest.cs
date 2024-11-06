using System.ComponentModel.DataAnnotations;

namespace VirtualArtGallery.WepApi.Models
{
    public class AddArtworkRequest
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public List<int> ExhibitionIds { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
