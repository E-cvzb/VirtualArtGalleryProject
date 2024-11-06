using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.Bussiness.DataProtection;
using VirtualArtGallery.Bussiness.Operations.Artwork.Dtos;

namespace VirtualArtGallery.Bussiness.Operations.Artwork
{
    public interface IArtworkService
    {
        Task<ServiceMessage> AddArtwork (ArtworkDto artworkDto);
        Task<ArtworkInfoDto> GetArtwork(int id);
        Task<List<ArtworkInfoDto>> GetAllArtwork();
        Task<ServiceMessage> Changetwork(int id , decimal priceTo);
        Task<ServiceMessage> DeleteAretwork(int id);
        Task<ServiceMessage >UpdateArtwork(UpdateArtworkDto artworkDto);
    }
}
