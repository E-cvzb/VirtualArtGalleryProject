using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using VirtualArtGallery.Bussiness.Operations.Artwork;
using VirtualArtGallery.Bussiness.Operations.Artwork.Dtos;
using VirtualArtGallery.WepApi.Filters;
using VirtualArtGallery.WepApi.Models;

namespace VirtualArtGallery.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtworkController : ControllerBase
    {
        private readonly IArtworkService _artworkSerice;
        public ArtworkController(IArtworkService artworkSerice)
        {
            _artworkSerice = artworkSerice;
        }
        [HttpPost]//Post ile sanat eseri ekleme yapıyoruz 
        [Authorize(Roles = ("Admin,Artist"))]//Yetkilendirme yapıyoruz
        [TimeControlFilter]
        public async Task<IActionResult> AddArtwork(AddArtworkRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var artworkDto = new ArtworkDto//verileri managere aktarmak için dto kullanacağım 
            {
                Name = request.Name,
                Price = request.Price,
                ExhibitionIds = request.ExhibitionIds,
                UserId = request.UserId,

            };
            var result = await _artworkSerice.AddArtwork(artworkDto);//Manager de ki metotu kullanarak ekleme yapıyorum 
            if (!result.IsSuccseed)//Managerden gelen mesaja göre ekleme yapılıp yapılmadığını belirtiyoruz 
                return BadRequest(result.Message);
            else
                return Ok(result);

        }

        [HttpGet("{id}")]//id si verilen veriyi get ile kullanıcıya gösteriyoruz
        public async Task<IActionResult> GetArtwork(int id)
        {
            var artwork = await _artworkSerice.GetArtwork(id);
            if (artwork is null)
                return NotFound();
            else
                return Ok(artwork);


        }
        [HttpGet]//Bütün verileri kullanıcıya gösteriyoruz
        public async Task<IActionResult> GetAllArtworks()
        {
            var artworks = await _artworkSerice.GetAllArtwork();

            return Ok(artworks);
        }
        [HttpPatch("{id}/price")]
        [Authorize(Roles = "Admin,Artist")]
        // Bir veride değişiklik yapmak istiyoruz bunu patch ile yaparız 
        public async Task<IActionResult> UpdatePrice(int id,decimal priceTo)//kullanıcı id ve pirice değişkeniniş girerek price verisini değiştiriyor
        {
         var result = await  _artworkSerice.Changetwork(id,priceTo);

            if (!result.IsSuccseed)//Managerden gelen veriye göre kullanıcıya mesaj dönüyoruz
            {
                return NotFound();

            }
            else
            {
                return Ok();
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Artist")]
        //İd si verilen veriyi silme 
        public async Task<IActionResult> DeleteArtwork(int id)
        {
            var control=  await _artworkSerice.DeleteAretwork(id);//id yi silme metotuna gönderiyoruz
            if (!control.IsSuccseed)//Managerden gelen verilere göre kullanıcıya mesaj dönüyoruz
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }
      
        [HttpPut]
        [Authorize(Roles ="Admin,Artist")]
        [TimeControlFilter]
        //Sanat eserinin bütün bilgilerini düzenlemek için yetkilendirme gerekmekte 
        public async Task<IActionResult> UpdateArtwork(int id , UpdateArtworkRequest request)
        {//Veriler kullanıcıdan request ile alınır 
            var updateArtworkDto = new UpdateArtworkDto
            {
                Id = id,
                Name = request.Name,
                Price = request.Price,
                ExhibitionIds = request.ExhibitionIds,
                UserId=request.UserId
            };//manager e gidecek veriler dto ya aktarılır 

            var result = await _artworkSerice.UpdateArtwork(updateArtworkDto);

            if (!result.IsSuccseed)
            {
                return NotFound(result.Message);
            }
            else
            {
                return await GetArtwork(id);//kullanıcıya değiştirilen yeni sanat eseri GetArtwork metotu ile kullanıcıya gösterilir
            }
            
        }
    }
}
