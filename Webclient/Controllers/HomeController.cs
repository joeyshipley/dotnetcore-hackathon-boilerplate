using System.Diagnostics;
using BOS.Webclient.Infrastructure.Controllers;
using BOS.Webclient.Models;
using Microsoft.AspNetCore.Mvc;

namespace BOS.Webclient.Controllers
{
    public class HomeController : ApplicationControllerBase
    {
        public IActionResult Index()
        {
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
