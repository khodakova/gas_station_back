using gas_station.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace gas_station.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DBContext _DBContext;

        public HomeController(ILogger<HomeController> logger, DBContext DBContext)
        {
            _logger = logger;
            _DBContext = DBContext;
        }

        public IActionResult Index()
        {
            ViewData.Model = _DBContext.StationModels.OrderBy(item=>item.id).ToList();
            return View();
        }

        public IActionResult GetStation(int id)
        {
            ViewData.Model = _DBContext.StationModels.FirstOrDefault(i => i.id == id);
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
