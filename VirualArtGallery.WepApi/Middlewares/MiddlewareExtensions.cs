namespace VirtualArtGallery.WepApi.Middlewares
{
    public static class MiddlewareExtensions
    {
        public  static IApplicationBuilder UseMiddlewareMode(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MaintenanceMiddleware>(); 
        }
    }
}
