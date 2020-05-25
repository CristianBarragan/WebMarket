using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Model.Api;

namespace WebMarket.Pipeline.ItemLogic.Process
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
            if (parameters.Model == null)
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Model is empty", "");
                return Task.FromResult(parameters);
            }

            if (string.IsNullOrEmpty(parameters.Model.Name))
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Name is not valid", "");
                return Task.FromResult(parameters);
            }

            logger.LogDebug("Item validated");

            return Task.FromResult(parameters);
        }
    }
}
