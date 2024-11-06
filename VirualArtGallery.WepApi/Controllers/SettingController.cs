using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualArtGallery.Bussiness.Setting;

namespace VirtualArtGallery.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _settingService;
        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpPatch]//bakıma almak için kuruldu bu yapı
        public async Task<IActionResult> ToggleMaintenence()
        {
            await _settingService.ToggleMaintenence();
            return Ok();
        }
    }
}
