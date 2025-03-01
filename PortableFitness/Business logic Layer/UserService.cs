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
				return (true, string.Empty);
			}
			else
			{
				string errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
				return (false, errorMessage);
			}

		}


		public async Task<(bool Success, string ErrorMessage)> LoginUserAsync(User user)
		{

			return (false, "heh");
		}
	}
}
