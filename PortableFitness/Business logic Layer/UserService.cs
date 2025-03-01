using Database.DBModelCommands;
using FitnessLogic.Models;
using Microsoft.AspNetCore.Identity;


namespace Business_logic_Layer
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public RoleManager<User> _roleManager { get; }

        public UserManager<User> _userManager { get; }

        public SignInManager<User> _signInManager { get; }
        public UserService(RoleManager<User> roleManager, SignInManager<User> signInManager)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<(bool Success, string ErrorMessage)> RegisterUserAsync(User user)
        {
            if (!await _userRepository.IsUsernameUniqueAsync(user.UserName))
            {
                return (false, "Это имя пользователя уже занято. Пожалуйста, выберите другое.");
            }
            var result = await _userManager.CreateAsync(user, user.PasswordHash);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return (true, string.Empty);
            }
            else
            {
                return (false, "_userManager didn't create a user");
            }

        }


        public async Task<(bool Success, string ErrorMessage)> LoginUserAsync(User user)
        {

            return (false, "heh");
        }
    }
}
