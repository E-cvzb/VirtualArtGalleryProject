using Azure.Core.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Bussiness.Operations.Artwork.Dtos
{
    public class ArtworkDto
    {
        public decimal Price { get; set; }
        public string Name { get; set; }
        public List<int> ExhibitionIds { get; set; }

        public int UserId { get; set; }
    }
}
