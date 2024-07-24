using Juan.Data;
using Juan.Models;
using Juan.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Juan.Controllers
{
    public class HomeController : Controller
    {
        private readonly JuanDbContext _context;

        public HomeController(JuanDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM();
            homeVM.sliders = await _context.Sliders.ToListAsync();
            return View(homeVM);
        }

    }
}
