using noone.ApplicationDTO.ApplicationUserDTO;
using noone.Models;

namespace noone.Reposatories.AuthenticationReposatory
{
    public interface IAuthenticationReposatory
    {
        Task<AuthenticationModel> RegisetrAsync(ApplicationUserRegisterDTO userRegisterDTO);
      
    }
}
