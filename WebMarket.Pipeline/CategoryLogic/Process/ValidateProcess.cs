using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Model.Api;

namespace WebMarket.Pipeline.CategoryLogic.Process
{
    public class ValidateProcess : IProcess<CategoryParameters>
    {
        private readonly ILogger<CategoryParameters> logger;

        public ValidateProcess(ILogger<CategoryParameters> logger)
        {
            this.logger = logger;
        }

        public Task<CategoryParameters> ExecuteAsync(CategoryParameters parameters)
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

            logger.LogDebug("Category validated");

            return Task.FromResult(parameters);
        }
    }
}
