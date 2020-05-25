using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Model.Api;

namespace WebMarket.Pipeline.ItemLogic.Process.Get
{
    public class ValidateProcess : IProcess<ItemParameters>
    {
        private readonly ILogger<ItemParameters> logger;

        public ValidateProcess(ILogger<ItemParameters> logger)
        {
            this.logger = logger;
        }

        public Task<ItemParameters> ExecuteAsync(ItemParameters parameters)
        {
            if ((parameters.ItemId == 0) && string.IsNullOrEmpty(parameters.Name))
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Item name not valid", "");
                return Task.FromResult(parameters);
            }

            if (parameters.ItemId <= 0)
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Item id not valid", "");
                return Task.FromResult(parameters);
            }

            logger.LogDebug("Item validated");

            return Task.FromResult(parameters);
        }
    }
}
