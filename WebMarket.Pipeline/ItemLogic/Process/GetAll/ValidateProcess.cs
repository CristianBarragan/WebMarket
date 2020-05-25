using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Model.Api;

namespace WebMarket.Pipeline.ItemLogic.Process.GetAll
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

            logger.LogDebug("Item validated");

            return Task.FromResult(parameters);
        }
    }
}
