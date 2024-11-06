using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.Bussiness.DataProtection;
using VirtualArtGallery.Bussiness.Operations.Exhibition.Dtos;
using VirtualArtGalleryProject.Data.Entities;
using VirtualArtGalleryProject.Data.Repository;
using VirtualArtGalleryProject.Data.UnitOfWork;

namespace VirtualArtGallery.Bussiness.Operations.Exhibition
{
    public class ExhibitionManager : IExhibitionService
    {
        private readonly IRepository<ExhibitionEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;
        public ExhibitionManager(IRepository<ExhibitionEntity> repository,IUnitOfWork unitOfWork)
        {
            
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ServiceMessage> AddExhibition(AddExhibitionDto addDto)
        {
           var control = _repository.GetAll(x=>x.Name.ToLower()==addDto.Name.ToLower()).Any();

            if(control)
            {
                return new ServiceMessage
                {
                    IsSuccseed = false,
                    Message="Daha önce böyle bir sergi düzenlendi lütfen farlı bir isim düşünün"
                };

            }

            var entity = new ExhibitionEntity
            {
                Date = addDto.Date,
                Name = addDto.Name,
            };

            _repository.Add(entity);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Ekleme sırasında bir hata oluştu tekrar oluşturun lütfen");
            }

            return new ServiceMessage
            {
                IsSuccseed = true,
            };


        }

        public async Task<List<ExhibitionInfoDto>> GetAllExhibition()
        {
            var exhibitions = await _repository.GetAll()
                   .Select(x => new ExhibitionInfoDto
                   {
                       Date = x.Date,
                       Name = x.Name,
                   }).ToListAsync();

            return exhibitions;
        }
   

        public async Task<ExhibitionInfoDto> GetExhibition(int id)
        {
            var exhibition = await _repository.GetAll(x => x.Id == id)
                 .Select(x => new ExhibitionInfoDto
                 {
                     Date = x.Date,
                     Name = x.Name,
                 }).FirstOrDefaultAsync();

            return exhibition ;
        }
    }
}
