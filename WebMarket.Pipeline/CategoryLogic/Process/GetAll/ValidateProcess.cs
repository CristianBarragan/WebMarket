using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Model.Api;

namespace WebMarket.Pipeline.CategoryLogic.Process.GetAll
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

            logger.LogDebug("Category validated");

            return Task.FromResult(parameters);
        }
    }
}
