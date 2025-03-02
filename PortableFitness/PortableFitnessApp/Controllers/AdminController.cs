using Microsoft.AspNetCore.Mvc;

namespace PortableFitnessApp.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
