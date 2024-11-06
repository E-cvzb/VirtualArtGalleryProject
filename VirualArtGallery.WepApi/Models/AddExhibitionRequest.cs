
using System.ComponentModel.DataAnnotations;

namespace VirtualArtGallery.WepApi.Models
{
    public class AddExhibitionRequest
    {
        [Required]
        [Length(3,25)]
        public string Name { get; set; }
        [Required]
        public string Date { get; set; }
    }
}
