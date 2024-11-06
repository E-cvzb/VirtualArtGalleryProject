using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.Bussiness.DataProtection;
using VirtualArtGallery.Bussiness.Operations.Exhibition.Dtos;

namespace VirtualArtGallery.Bussiness.Operations.Exhibition
{
    public interface IExhibitionService
    {
        Task<ServiceMessage> AddExhibition(AddExhibitionDto addDto);
        Task<ExhibitionInfoDto> GetExhibition(int id);
        Task<List<ExhibitionInfoDto>> GetAllExhibition();
    }
}
