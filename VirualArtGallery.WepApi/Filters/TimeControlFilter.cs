using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace VirtualArtGallery.WepApi.Filters
{
    public class TimeControlFilter:ActionFilterAttribute
    {

        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {


            var now = DateTime.Now.TimeOfDay;


            StartTime = "05:00";
            EndTime = "12:00";

            if (now >=TimeSpan.Parse(StartTime) && now <=TimeSpan.Parse(EndTime))
            {
                base.OnActionExecuting(context);    
            }
            else
            {
                context.Result = new ContentResult
                {
                    Content = "Bu saatler arasında hizmet verememekteyiz",
                    StatusCode = 403,

                };
            }


            
        }
    }
}
