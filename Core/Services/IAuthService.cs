using System.Threading.Tasks;
using ITSchool.Core.DTOs;

namespace ITSchool.Core.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Login(LoginDto loginDto);
    }
}
