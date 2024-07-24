using Juan.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Juan.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}
