using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Core.Extensions;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
		public HomeController()
		{
		}
		public IActionResult Index()
        {
			return View();
        }

		public IActionResult Error()
        {
            return View();
        }
    }
}
