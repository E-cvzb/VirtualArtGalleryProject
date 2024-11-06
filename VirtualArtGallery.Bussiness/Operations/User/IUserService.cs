using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGallery.Bussiness.DataProtection;
using VirtualArtGallery.Bussiness.Operations.User.Dtos;

namespace VirtualArtGallery.Bussiness.Operations.User
{
    public interface IUserService
    {
        Task<ServiceMessage> AddUser(AddUserDto user);
        ServiceMessage <UserInfoDto> LoginUser(UserLoginDto user);


    }
}
