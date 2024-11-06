using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.Bussiness.DataProtection;
using VirtualArtGallery.Bussiness.Operations.User.Dtos;
using VirtualArtGalleryProject.Data.Entities;
using VirtualArtGalleryProject.Data.Repository;
using VirtualArtGalleryProject.Data.UnitOfWork;

namespace VirtualArtGallery.Bussiness.Operations.User
{
    public class UserManager : IUserService
    {
       private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserEntity> _userRepository;
       
        private readonly IDataProdection _dataProdection;
        public UserManager(IUnitOfWork unitOfWork,IRepository<UserEntity> userRepository,IDataProdection dataProdection)
        {
            
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _dataProdection = dataProdection;
           
        }
        public async Task<ServiceMessage> AddUser(AddUserDto user)
        {
            
          var email =_userRepository.GetAll(x=>x.Email.ToLower()==user.Email.ToLower());
            if (email.Any())
            {
                return new ServiceMessage
                {
                    IsSuccseed = false,
                    Message = "Mail adresi kullanılıyor "
                };
            }
            var userEntity = new UserEntity
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Password = _dataProdection.Protect(user.Password),//Kullanıcının şifresini şifreleme işlemi
            };
            _userRepository.Add(userEntity);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Kullanıcı kayıdı sırasında bir hata oluştu");
            }
            return new ServiceMessage
            {
                IsSuccseed = true,
            };
        }

        public  ServiceMessage<UserInfoDto> LoginUser(UserLoginDto user)
        {
           var userEntity= _userRepository.Get(x=>x.Email.ToLower()== user.Email.ToLower());
            if (userEntity is null)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSuccseed = false,
                    Message = "Kullanıcı adı veya şifre hatalı"
                };
            }
            var unProtect=_dataProdection.UnProtect(userEntity.Password);//Veri tabanındaki şifrelenmiş şifreyi çözerek karşılaştırıyoruz
            if (unProtect == user.Password)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSuccseed = true,
                    Data = new UserInfoDto
                    {
                        Email = userEntity.Email,
                        FirstName =userEntity.FirstName,
                        LastName =userEntity.LastName,
                        UserType = userEntity.UserType,
                        DateOfBirth = userEntity.DateOfBirth,
                      Id=userEntity.Id
                    }
                };
            }
            else
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSuccseed = false,
                    Message = "Kullanıcı adı veya şifre hatalı"
                };
            }
        }
    }
}
