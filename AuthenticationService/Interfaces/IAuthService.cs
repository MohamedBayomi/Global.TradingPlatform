using Authentication_Service.DTOs;
using EmployeesPortal.Shared.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Authentication_Service.Interfaces
{
    public interface IAuthService
    {
        Task<AuthDTO> RegisterAsync(RegisterDTO model);
        Task<AuthDTO> LoginAsync(LoginDTO model);
        Task<AuthDTO> CheakResetPassword(CheckResetCodeDTO model);

    }
}
