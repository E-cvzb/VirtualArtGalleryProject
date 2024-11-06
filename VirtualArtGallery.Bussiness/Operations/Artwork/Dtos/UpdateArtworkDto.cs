using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Bussiness.Operations.Artwork.Dtos
{
    public class UpdateArtworkDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<ArtworkExhibitionDto> ExhibitionIds { get; set; }
        public int UserId { get; set; }
    }
}
