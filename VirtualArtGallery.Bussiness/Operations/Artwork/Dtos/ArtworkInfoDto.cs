using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Bussiness.Operations.Artwork.Dtos
{
    public class ArtworkInfoDto
    {
        public decimal Price { get; set; }
        public string Name { get; set; }
        public List<ArtworkExhibitionDto> Exhibition { get; set; } 

    }
}
