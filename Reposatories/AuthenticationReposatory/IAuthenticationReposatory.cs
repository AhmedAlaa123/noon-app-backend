using noone.ApplicationDTO.ApplicationUserDTO;
using noone.Models;

namespace noone.Reposatories.AuthenticationReposatory
{
    public interface IAuthenticationReposatory
    {

        //Login
        Task<AuthenticationModel> GetTokenAsync(ApplicationUserSignInDTO userSignInDTO);
        // Register
        Task<AuthenticationModel> RegisetrAsync(ApplicationUserRegisterDTO userRegisterDTO);
        //Sign Out
        Task<AuthenticationModel> SignOutAsync(string userName);
        // Add Role To User
        Task<string> AddUserToRole(ApplicationUserAddRoleDTO userAddRoleDTO);
        // Remove Role From User
        Task<string> RemoveRoleFromUser(ApplicationUserAddRoleDTO userAddRoleDTO);
        // Remove User
        Task<string> RemoveUser(string JWTToken, string deletedUserId);
        //Get All Users That are not admin
        Task<IEnumerable<ApplicationUserInfoDTO>> GetAllUsers();


    }
}
