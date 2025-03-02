using Business_logic_Layer;
using Database.Contexts;
using Database.DBModelCommands;
using FitnessLogic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PortableFitnessApp.DTO;

namespace PortableFitnessApp.Controllers
{
	public class UserController : Controller
	{

		public AppDbContext UserDataBase { get; set; }
		public UserRepository UserRepository { get; private set; }

		public UserService UserService { get; set; }
		public UserController(AppDbContext userDataBase, UserRepository userRepository, UserService userService)
		{
			UserDataBase = userDataBase;
			UserRepository = userRepository;
			UserService = userService;
		}
		[HttpGet]
		public ActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<ActionResult> Register(UserRegisterDto model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			try
			{
				// преобразовать в сервисе
				User user1 = (User)model;
				// я не хочу просто передавать DTO в сервисы либо делать зависимость. 
				var result=await UserService.RegisterUserAsync(model);
				if (result.Success)
				{
					//добавить роли или 
					return RedirectToAction("Index", "Home");
				}
				else
				{
					throw new Exception(result.ErrorMessage);
				}
			}
			catch (Exception ex)
			{

				ModelState.AddModelError("", $"Ошибка при регистрации: {ex.Message}");
				
			}
			return View(model);
		}
		//Авторизация 
		[HttpGet]
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Login(UserLoginDto model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var result = await UserService.LoginUserAsync(model);

				if (result.Success)
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
					throw new Exception(result.ErrorMessage);
				}
			}
			catch (Exception ex)
			{
                ModelState.AddModelError("", $"Ошибка при регистрации: {ex.Message}");
            }
            return View(model);
        }
		[HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminDashboard()
        {
            return View();
        }




        // GET: UserController/Details/5
        public ActionResult Details(int id)
		{
			return View();
		}

		// GET: UserController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: UserController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: UserController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: UserController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: UserController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: UserController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
