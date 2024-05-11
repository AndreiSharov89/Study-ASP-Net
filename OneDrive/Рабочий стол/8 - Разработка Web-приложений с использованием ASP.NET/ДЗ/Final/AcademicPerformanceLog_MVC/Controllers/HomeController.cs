using AcademicPerformanceLog_MVC.Data;
using AcademicPerformanceLog_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AcademicPerformanceLog_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PerformanceLogContext db;

        public HomeController(ILogger<HomeController> logger, PerformanceLogContext context)
        {
            _logger = logger;
            db = context;
        }

        public IActionResult Index()
        {
            var allDisciplines = db.Disciplines.ToList<Discipline>();
            ViewBag.Disciplines = allDisciplines;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
