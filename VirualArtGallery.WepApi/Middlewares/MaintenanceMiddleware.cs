using VirtualArtGallery.Bussiness.Setting;

namespace VirtualArtGallery.WepApi.Middlewares
{
    public class MaintenanceMiddleware
    {

        private readonly RequestDelegate _next;
        
        public MaintenanceMiddleware(RequestDelegate next)
        {
            _next= next;
            
        }
        public async Task Invoke(HttpContext context)
        {

            var settingService=context.RequestServices.GetRequiredService<ISettingService>();   
            bool maintenanceMode = settingService.GetMaintenanceState();

            if(context.Request.Path.StartsWithSegments("/api/Setting")||
                context.Request.Path.StartsWithSegments("/api/Auth/Login"))
            {
                await _next(context);
                return;

            }


            if(maintenanceMode)
            {
                await context.Response.WriteAsync("Şuan hizmet verememekteyiz.");
            }
            else
            {
                await _next(context);
            }
        }
    }
}
