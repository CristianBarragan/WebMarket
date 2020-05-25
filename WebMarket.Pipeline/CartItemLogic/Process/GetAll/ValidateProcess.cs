using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Model.Api;

namespace WebMarket.Pipeline.CartItemLogic.Process.GetAll
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
            if (parameters.PageSize <= 0)
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Invalid page size", "");
                return Task.FromResult(parameters);
            }

            if (parameters.Page <= 0)
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Invalid page", "");
                return Task.FromResult(parameters);
            }

            logger.LogDebug("Car Item validated");

            return Task.FromResult(parameters);
        }
    }
}
