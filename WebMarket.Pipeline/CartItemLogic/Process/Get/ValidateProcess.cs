using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Model.Api;

namespace WebMarket.Pipeline.CartItemLogic.Process.Get
{
    public class ValidateProcess : IProcess<CartItemParameters>
    {
        private readonly ILogger<CartItemParameters> logger;

        public ValidateProcess(ILogger<CartItemParameters> logger)
        {
            this.logger = logger;
        }

        public Task<CartItemParameters> ExecuteAsync(CartItemParameters parameters)
        {
            if (parameters.Model == null)
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Model is empty", "");
                return Task.FromResult(parameters);
            }

            if (parameters.Model.OrderId <= 0)
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Order Id not valid", "");
                return Task.FromResult(parameters);
            }

            if (parameters.Model.ProductId <= 0)
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Product id not valid", "");
                return Task.FromResult(parameters);
            }

            logger.LogDebug("Car Item validated");

            return Task.FromResult(parameters);
        }
    }
}
