using System.Collections.Generic;
using _3abarni_backend.DTOs;
using _3abarni_backend.Mappers;
using _3abarni_backend.Models;
using _3abarni_backend.Repositories;

namespace _3abarni_backend.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<UserDto> GetAll()
        {
            var users = _userRepository.GetAll();
            return users.Select(UserMapper.MapToDto);
        }

        public UserDto GetById(string id)
        {
            var user = _userRepository.GetById(id);
            return user != null ? UserMapper.MapToDto(user) : null;
        }

        public void Create(UserDto userDto)
        {
            var user = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                ProfilePicPath = userDto.ProfilePicPath
                // You might need to set other properties as needed
            };
            _userRepository.Create(user);
        }

        public void Update(string id, UserDto userDto)
        {
            var existingUser = _userRepository.GetById(id);
            if (existingUser != null)
            {
                existingUser.UserName = userDto.UserName;
                existingUser.Email = userDto.Email;
                existingUser.ProfilePicPath = userDto.ProfilePicPath;
                // Update other properties as needed
                _userRepository.Update(existingUser);
            }
        }

        public void Delete(string id)
        {
            _userRepository.Delete(id);
        }

        public IEnumerable<UserDto> SearchPaginated(string query, int page)
        {
            var users= _userRepository.SearchPaginated(query, page);
            return users != null ? users.Select(UserMapper.MapToDto) : new List<UserDto>();
        }

    }
}
