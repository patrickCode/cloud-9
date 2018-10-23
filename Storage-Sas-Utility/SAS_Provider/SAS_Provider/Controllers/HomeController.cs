using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAS.Provider.Core;
using SAS_Provider.Models;

namespace SAS_Provider.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }

        public string Token()
        {
            var storageSasProvider = new StorageSasProvider("fxprgconfit", "0JlbNBt6/5d2yEWU2o35oPXpFncaYNHY8mz5eF4HiSKe1IbKgKKC57Bh8+NznwYJYbm3be+w1t44E8wraS4FNQ==");
            var result = storageSasProvider.CreateSasForContainer("auroratest", 0.5).Result;
            return result;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
