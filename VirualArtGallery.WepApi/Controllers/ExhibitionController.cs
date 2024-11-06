using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;
using VirtualArtGallery.Bussiness.Operations.Exhibition;
using VirtualArtGallery.Bussiness.Operations.Exhibition.Dtos;
using VirtualArtGallery.WepApi.Models;

namespace VirtualArtGallery.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExhibitionController : ControllerBase
    {
       private readonly IExhibitionService _exhibitionService;
        public ExhibitionController(IExhibitionService exhibitionService)
        {
            
            _exhibitionService = exhibitionService;
        }




        [HttpPost]
        public async Task<IActionResult> AddExhibition(AddExhibitionRequest request)
        {
            var addDto = new AddExhibitionDto
            {
                Date = request.Date,
                Name = request.Name,
            };

            var result= await _exhibitionService.AddExhibition(addDto);

            if (!result.IsSuccseed)
            {
                return BadRequest(result.Message);
            }
            else
            {
                return Ok();
            }


        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetEchibition (int id)
        {
            var result = await _exhibitionService.GetExhibition(id);
            if (result is null)
                return BadRequest();
            else
                return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = _exhibitionService.GetAllExhibition();
            return Ok(result);
        }
        





    }
}
