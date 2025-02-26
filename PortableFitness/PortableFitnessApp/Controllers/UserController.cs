using Database.Contexts;
using Database.DBModelCommands;
using FitnessLogic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static PortableFitnessApp.DTO.UserRegistrationDTO;

namespace PortableFitnessApp.Controllers
{
    public class UserController : Controller
    {
        // GET: UserController
        public Database.Contexts.AppDbContext UserDataBase { get; set; }
        public UserRepository UserRepository { get; private set; }
        public UserController(Database.Contexts.AppDbContext userDataBase,UserRepository userRepository)
        {
            UserDataBase = userDataBase;
            UserRepository = userRepository;
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(UserRegisterDto user)
        {
            if (!ModelState.IsValid)
            {
                // плохая валидация- ошибка
                return BadRequest(ModelState);
            }

            User user1 = (User)user;
            await  UserRepository.CreateAsync(user1);
            return Ok();
        }
        //Авторизация и занос в бд
        [HttpGet]
        public ActionResult Authorize()
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
