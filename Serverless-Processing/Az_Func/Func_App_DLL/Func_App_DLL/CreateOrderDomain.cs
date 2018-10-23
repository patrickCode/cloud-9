using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using OrderDomain;
using System;
using Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Func_App_DLL
{
    public static class CreateOrderDomain
    {
        [FunctionName("CreateOrderDomain")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            string orderBody = new StreamReader(req.Body).ReadToEnd();
            var orderDto = JsonConvert.DeserializeObject<OrderDto>(orderBody);
            try
            {
                var order = new Order();
                order.CreateOrder(orderDto);

                orderDto.Id = order.Id;
                orderDto.OrderPrice = order.OrderPrice;
                orderDto.PlacedDate = order.PlacedDate;

                var result = new Result()
                {
                    Order = orderDto,
                    Events = order.GetUnsavedEvents().ToList()
                };
                order.SaveEvents();

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        internal class Result
        {
            public OrderDto Order;
            public List<Event> Events;
        }
    }
}
