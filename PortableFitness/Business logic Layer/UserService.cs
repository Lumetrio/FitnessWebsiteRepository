using Database.DBModelCommands;
using FitnessLogic.Models;
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

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
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
