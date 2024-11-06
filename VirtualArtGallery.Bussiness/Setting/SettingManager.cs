using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGalleryProject.Data.Entities;
using VirtualArtGalleryProject.Data.Repository;
using VirtualArtGalleryProject.Data.UnitOfWork;

namespace VirtualArtGallery.Bussiness.Setting
{
    public class SettingManager:ISettingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<SettingEntity> _repository;

        public SettingManager(IUnitOfWork unitOfWork,IRepository<SettingEntity> repository)
        {
            _repository = repository;
            _unitOfWork= unitOfWork;
                
        }

        public bool GetMaintenanceState()
        {
            var maintenanceState = _repository.GetById(1).MaintenenceMode;
            return maintenanceState;
        }

        public async Task ToggleMaintenence()
        {
            var setting = _repository.GetById(1);

            setting.MaintenenceMode = !setting.MaintenenceMode;
            _repository.Update(setting);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("İşlem sırasında bir hata oluştu ");
            }
            
        }
    }
}
