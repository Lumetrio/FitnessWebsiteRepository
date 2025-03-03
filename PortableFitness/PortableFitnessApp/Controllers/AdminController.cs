//using FitnessLogic.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;

//namespace PortableFitnessApp.Controllers
//{
//	public class AdminController : Controller
//	{
//		[Authorize(Roles = "Admin")]
//		public class UserAdminController : Controller
//		{
//			private readonly UserManager<User> _userManager;
//			private readonly RoleManager<IdentityRole> _roleManager;

//			public UserAdminController(
//				UserManager<User> userManager,
//				RoleManager<IdentityRole> roleManager)
//			{
//				_userManager = userManager;
//				_roleManager = roleManager;
//			}

//			public async Task<IActionResult> EditRoles(string userId)
//			{
//				var user = await _userManager.FindByIdAsync(userId);
//				if (user == null)
//				{
//					return NotFound();
//				}

//				var model = new EditUserRolesViewModel
//				{
//					UserId = userId,
//					UserName = user.UserName
//				};

//				// Получаем все роли и отмечаем те, которые есть у пользователя
//				foreach (var role in _roleManager.Roles)
//				{
//					model.Roles.Add(new RoleViewModel
//					{
//						RoleId = role.Id,
//						RoleName = role.Name,
//						IsSelected = await _userManager.IsInRoleAsync(user, role.Name)
//					});
//				}

//				return View(model);
//			}

//			[HttpPost]
//			public async Task<IActionResult> EditRoles(EditUserRolesViewModel model)
//			{
//				var user = await _userManager.FindByIdAsync(model.UserId);
//				if (user == null)
//				{
//					return NotFound();
//				}

//				// Получаем текущие роли пользователя
//				var userRoles = await _userManager.GetRolesAsync(user);

//				// Удаляем пользователя из всех ролей
//				await _userManager.RemoveFromRolesAsync(user, userRoles);

//				// Добавляем пользователя в выбранные роли
//				await _userManager.AddToRolesAsync(user,
//					model.Roles.Where(x => x.IsSelected).Select(x => x.RoleName));

//				return RedirectToAction("Index");
//			}
//		}
//	}
//}
