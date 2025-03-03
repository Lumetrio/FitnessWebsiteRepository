using Database.DBModelCommands;
using FitnessLogic.Models;
using Microsoft.AspNetCore.Identity;
using PortableFitnessApp.DTO;


namespace Business_logic_Layer
{
	public class UserService
	{
		public RoleManager<IdentityRole> _roleManager { get; }

		public UserManager<User> _userManager { get; }

		public SignInManager<User> _signInManager { get; }
		public UserService(RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager, UserManager<User> userManager)
		{
			_roleManager = roleManager;
			_signInManager = signInManager;
			_userManager = userManager;
		}
		public async Task<(bool Success, string ErrorMessage)> RegisterUserAsync(UserRegisterDto model)
		{

			User user = (User)model;
			var result = await _userManager.CreateAsync(user, model.Password);
			
			if (result.Succeeded)
			{
				await _signInManager.SignInAsync(user, isPersistent: false);
			
                await _userManager.AddToRoleAsync(user, "Admin");
                return (true, string.Empty);
			}
			else
			{
				string errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
				return (false, errorMessage);
			}

		}


		public async Task<(bool Success, string ErrorMessage)> LoginUserAsync(UserLoginDto model)
		{
		
            // Поиск пользователя по имени
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
                return (false,"Неверное имя пользователя или пароль");

            // Проверка пароля
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);
            if (!result.Succeeded)
                return (false,"Неверное имя пользователя или пароль");
            // Вход пользователя в систему
            await _signInManager.SignInAsync(user, model.RememberMe);
            return (true,"");
        }


		//// Создание ролей
		//public async Task CreateRoles(RoleManager<IdentityRole> roleManager)
		//{
		//	// у пользователя с confirmedEmail больше прав 
		//	string[] roleNames = { "Admin", "Moderator","User","ConfirmedUser" };

		//	foreach (var roleName in roleNames)
		//	{
		//		// если роль не существует.
		//		if (!await roleManager.RoleExistsAsync(roleName))
		//		{
		//			// Создаем роль
		//			await roleManager.CreateAsync(new IdentityRole(roleName));
		//		}
		//	}
		//}
	}
}
