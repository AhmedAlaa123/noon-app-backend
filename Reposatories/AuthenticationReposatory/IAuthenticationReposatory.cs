﻿using noone.ApplicationDTO.ApplicationUserDTO;
using noone.Models;

namespace noone.Reposatories.AuthenticationReposatory
{
    public interface IAuthenticationReposatory
    {

        //Login
        Task<AuthenticationModel> GetTokenAsync(ApplicationUserSignInDTO model);

        Task<AuthenticationModel> RegisetrAsync(ApplicationUserRegisterDTO userRegisterDTO);
      
    }
}
