
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExamFinal.Controllers
{
	public class HomeController : Controller
	{
		

		public IActionResult Index()
		{
			return View();
		}

		
	}
}
