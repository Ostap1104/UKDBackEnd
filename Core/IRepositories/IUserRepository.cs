using System.Threading.Tasks;
using ITSchool.Core.DTOs;

namespace ITSchool.Core.IRepositories
{
    public interface IUserRepository
    {
        Task<UserDto> GetUserByUsernameAsync(string username);
        Task<bool> ValidatePasswordAsync(string username, string password);
        string HashPassword(string password);
    }
}
