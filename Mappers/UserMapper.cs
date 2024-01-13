using _3abarni_backend.DTOs;
using _3abarni_backend.Models;

namespace _3abarni_backend.Mappers
{
    public static class UserMapper
    {
        public static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                ProfilePicPath = user.ProfilePicPath
            };
        }
    }

}
