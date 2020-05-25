using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Model.Api;

namespace WebMarket.Pipeline.CartItemLogic.Process.Delete
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

            if (parameters.OrderId <= 0)
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Order Id not valid", "");
                return Task.FromResult(parameters);
            }

            if (parameters.ProductId <= 0)
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Product id not valid", "");
                return Task.FromResult(parameters);
            }

            logger.LogDebug("Cart Item validated");

            return Task.FromResult(parameters);
        }
    }
}
