using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Pizzaria.Function.Api.Processor.Interface;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Pizzaria.Function.Api.Controller
{
    public class ProductController
    {
        private readonly ILogger<ProductController> logger;
        private readonly IProductProcessor productProcessor;

        public ProductController(ILogger<ProductController> logger, IProductProcessor productProcessor)
        {
            this.logger = logger;
            this.productProcessor = productProcessor;
        }

        [FunctionName("getProduct")]
        public async Task<IActionResult> GetProductByIdAsync(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "product/{productId}")] HttpRequest req, int productId)
        {
            this.logger.LogInformation($"{nameof(ProductController)} => GetProductByIdAsync : Starts");
            try
            {
                this.logger.LogInformation($"Get Product By {productId} request received");

                var product = await this.productProcessor.GetProductByIdAsync(productId).ConfigureAwait(false);

                return new OkObjectResult(product);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{nameof(ProductController)} => GetProductByIdAsync : Exception Occured   : {ex.Message}");
                return new InternalServerErrorResult();
            }
            finally
            {
                this.logger.LogInformation($"{nameof(ProductController)} => GetProductByIdAsync : Ends");
            }
        }

        [FunctionName("menu")]
        public async Task<IActionResult> GetMenuAsync(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "menu/all")] HttpRequest req)
        {
            this.logger.LogInformation($"{nameof(ProductController)} => GetMenuAsync : Starts");
            try
            {
                this.logger.LogInformation($"Get All Product request received");

                var products = await this.productProcessor.GetMenuAsync().ConfigureAwait(false);

                return new OkObjectResult(products);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{nameof(ProductController)} => GetMenuAsync : Exception Occured   : {ex.Message}");
                return new InternalServerErrorResult();
            }
            finally
            {
                this.logger.LogInformation($"{nameof(ProductController)} => GetMenuAsync : Ends");
            }
        }

        [FunctionName("allCategories")]
        public async Task<IActionResult> GetAllCategoriesAsync(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "category/all")] HttpRequest req)
        {
            this.logger.LogInformation($"{nameof(ProductController)} => GetAllCategoriesAsync : Starts");
            try
            {
                this.logger.LogInformation($"Get All Categories request received");

                var categories = await this.productProcessor.GetAllCategoriesAsync().ConfigureAwait(false);

                return new OkObjectResult(categories);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{nameof(ProductController)} => GetAllCategoriesAsync : Exception Occured   : {ex.Message}");
                return new InternalServerErrorResult();
            }
            finally
            {
                this.logger.LogInformation($"{nameof(ProductController)} => GetAllCategoriesAsync : Ends");
            }
        }

        [FunctionName("allCrust")]
        public async Task<IActionResult> GetAllCrustAsync(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "crust/all")] HttpRequest req)
        {
            this.logger.LogInformation($"{nameof(ProductController)} => GetAllCrustAsync : Starts");
            try
            {
                this.logger.LogInformation($"Get All Crusts request received");

                var crusts = await this.productProcessor.GetAllCrustsAsync().ConfigureAwait(false);

                return new OkObjectResult(crusts);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{nameof(ProductController)} => GetAllCrustAsync : Exception Occured   : {ex.Message}");
                return new InternalServerErrorResult();
            }
            finally
            {
                this.logger.LogInformation($"{nameof(ProductController)} => GetAllCrustAsync : Ends");
            }
        }

        [FunctionName("allToppings")]
        public async Task<IActionResult> GetAllToppingsAsync(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "topping/all")] HttpRequest req)
        {
            this.logger.LogInformation($"{nameof(ProductController)} => GetAllToppingsAsync : Starts");
            try
            {
                this.logger.LogInformation($"Get All Toppings request received");

                var toppings = await this.productProcessor.GetAllToppingsAsync().ConfigureAwait(false);

                return new OkObjectResult(toppings);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{nameof(ProductController)} => GetAllToppingsAsync : Exception Occured   : {ex.Message}");
                return new InternalServerErrorResult();
            }
            finally
            {
                this.logger.LogInformation($"{nameof(ProductController)} => GetAllToppingsAsync : Ends");
            }
        }

        [FunctionName("allSize")]
        public async Task<IActionResult> GetAllSizesAsync(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "size/all")] HttpRequest req)
        {
            this.logger.LogInformation($"{nameof(ProductController)} => GetAllSizesAsync : Starts");
            try
            {
                this.logger.LogInformation($"Get All Sizes request received");

                var sizes = await this.productProcessor.GetAllSizesAsync().ConfigureAwait(false);

                return new OkObjectResult(sizes);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{nameof(ProductController)} => GetAllSizesAsync : Exception Occured   : {ex.Message}");
                return new InternalServerErrorResult();
            }
            finally
            {
                this.logger.LogInformation($"{nameof(ProductController)} => GetAllSizesAsync : Ends");
            }
        }
    }
}
