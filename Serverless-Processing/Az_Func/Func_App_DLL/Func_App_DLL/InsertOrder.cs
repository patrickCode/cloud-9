using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Contracts;
using Repository;

namespace Func_App_DLL
{
    public static class InsertOrder
    {
        [FunctionName("InsertOrder")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string orderBody = new StreamReader(req.Body).ReadToEnd();
            var orderDto = JsonConvert.DeserializeObject<OrderDto>(orderBody);

            var orderRepository = new OrderRepository("https://doc-quest.documents.azure.com:443/", "7Y9l4npDQyiTD2eQsRwZZgy8RwV4LXIMofCE0wDswegPf7IH93iVlTg815KRVlSL0TlGm5T3SzMBmP5Pkuan0w==");

            await orderRepository.AddOrder(orderDto);
            return new OkObjectResult(orderDto);
        }
    }
}
