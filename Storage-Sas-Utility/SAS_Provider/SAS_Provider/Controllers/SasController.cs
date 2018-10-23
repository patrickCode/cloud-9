using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAS.Provider.Core;
using SAS.Provider.Web.Models;
using SAS_Provider.Models;

namespace SAS_Provider.Controllers
{
    [Authorize]
    public class SasController : Controller
    {
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Token(BlobContainerSasRequestModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var storageSasProvider = new StorageSasProvider(model.StorageAccountName, model.StorageAccountKey);
                    var token = storageSasProvider.CreateSasForContainer(model.BlobContainerName, model.ActivationPeriod).Result;
                    return RedirectToAction("TokenSuccess", new { Token = token, ExpireAt = DateTime.Now.AddHours(model.ActivationPeriod).ToString() });
                }
                return RedirectToAction("TokenFailure", new { Error = "Some error in generating the token" });
            }
            catch (Exception error)
            {
                return RedirectToAction("TokenFailure", new { Error = error.Message });
            }
        }

        public IActionResult TokenSuccess(string token, string expireAt)
        {
            ViewData["Token"] = token;
            ViewData["ExpireAt"] = expireAt;
            return View();
        }

        public IActionResult TokenFailure(string error)
        {
            ViewData["Error"] = error;
            return View();
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
