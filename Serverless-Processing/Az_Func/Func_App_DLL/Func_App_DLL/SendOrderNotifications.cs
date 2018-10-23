using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using Contracts;
using NotificationService;

namespace Func_App_DLL
{
    public static class SendOrderNotifications
    {
        [FunctionName("SendOrderNotifications")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var events = JsonConvert.DeserializeObject<List<Event>>(requestBody);

            var notificationManager = new NotificationManager("https://orderchannel.southindia-1.eventgrid.azure.net/api/events", "K54SnMuQwRjqQkzYZVAfTNJNb1Iuli9N4eFog+FJtw0=");
            await notificationManager.SendOrderNotifications(events);

            return new OkObjectResult(events);
        }
    }
}
