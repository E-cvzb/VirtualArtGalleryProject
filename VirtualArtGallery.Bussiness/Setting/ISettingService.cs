using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Bussiness.Setting
{
    public interface ISettingService
    {
        Task ToggleMaintenence();
        bool GetMaintenanceState();
    }
}
