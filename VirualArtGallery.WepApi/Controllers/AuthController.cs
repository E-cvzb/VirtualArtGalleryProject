using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using VirtualArtGallery.Bussiness.Operations.User;
using VirtualArtGallery.Bussiness.Operations.User.Dtos;
using VirualArtGallery.WepApi.Jwt;
using VirualArtGallery.WepApi.Models;
using LoginRequest = VirualArtGallery.WepApi.Models.LoginRequest;
using RegisterRequest = VirualArtGallery.WepApi.Models.RegisterRequest;

namespace VirualArtGallery.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        //Kullanıcı kayıt sistemi
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addUserDto = new AddUserDto {
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
            };
            var result = await _userService.AddUser(addUserDto);

            if(result.IsSuccseed)
                return Ok();
            else
                return BadRequest(result);
        }
        [HttpPost("Login")]
        //Kullanıcı girişi
        public IActionResult Login (LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();   
            }
            var result = _userService.LoginUser(new UserLoginDto { Email=request.Email, Password=request.Password});
            if (!result.IsSuccseed)
                return BadRequest(result.Message);

         var user=result.Data;
            var configuration =HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var token = JwtHelper.GenerateJwtToken(new JwtDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserType = user.UserType,
                SecretKey = configuration["Jwt:SecretKey"]!,
                Issuer = configuration["Jwt:Issuer"]!,
                Audience = configuration["Jwt:Audience"]!,
                ExpireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"])

            });
            
            return Ok(new LoginResponse
            {
                Massege="Giriş başarı ile yapıldı",
                Token=token,

                
            });


        }
        [HttpGet("Me")]
        [Authorize]
        public IActionResult GetMyUser()
        {
            return Ok();
        }
    }
}
