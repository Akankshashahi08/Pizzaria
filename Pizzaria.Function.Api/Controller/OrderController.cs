using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Pizzaria.Entities.DataModels;
using Pizzaria.Function.Api.Processor.Interface;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Pizzaria.Function.Api.Controller
{
    public class OrderController
    {
        private readonly ILogger<OrderController> logger;

        private readonly IOrderProcessor orderProcessor;

        public OrderController(ILogger<OrderController> logger, IOrderProcessor orderProcessor)
        {
            this.logger = logger;
            this.orderProcessor = orderProcessor;
        }

        [FunctionName("getOrder")]
        public async Task<IActionResult> GetOrderByIdAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "order/{orderId}")] HttpRequest req, int orderId)
        {
            this.logger.LogInformation($"{nameof(OrderController)} => GetOrderByIdAsync : Starts");

            try
            {
                this.logger.LogInformation($"Get Order By {orderId} request received");

                var order = await this.orderProcessor.GetOrderByIdAsync(orderId).ConfigureAwait(false);

                return new OkObjectResult(order);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{nameof(OrderController)} => GetOrderByIdAsync : Exception Occured   : {ex.Message}");
                return new InternalServerErrorResult();
            }
            finally
            {
                this.logger.LogInformation($"{nameof(OrderController)} => GetOrderByIdAsync : Ends");
            }
        }

        [FunctionName("saveOrder")]
        public async Task<IActionResult> SaveOrderAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "order/save")] HttpRequest req)
        {
            this.logger.LogInformation($"{nameof(OrderController)} => SaveOrderAsync : Starts");
            try
            {
                this.logger.LogInformation("Save Order request received.");

                var data = await req.GetObjectAsync<Order>().ConfigureAwait(false);

                var newOrder = await this.orderProcessor.SaveOrderAsync(data).ConfigureAwait(false);

                return new OkObjectResult(newOrder);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{nameof(OrderController)} => SaveOrderAsync : Exception Occured   : {ex.Message}");
                return new InternalServerErrorResult();
            }
            finally
            {
                this.logger.LogInformation($"{nameof(OrderController)} => SaveOrderAsync : Ends");
            }
        }
    }
}
