using Database.DBModelCommands;
using FitnessLogic.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_logic_Layer
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
		private readonly IPasswordHasher<UserRegisterDto> _passwordHasher;
		public UserService(UserRepository userRepository, IPasswordHasher<User> passwordHasher)
		{
			_userRepository = userRepository;
			_passwordHasher = passwordHasher;
		}

		public async Task<(bool Success, string ErrorMessage)> RegisterUserAsync(User user)
        {
            if (!await _userRepository.IsUsernameUniqueAsync(user.Name))
            {
                return (false, "Это имя пользователя уже занято. Пожалуйста, выберите другое.");
            }

            await _userRepository.CreateAsync(user);
            return (true, string.Empty);
        }
    }
}
