using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Model.Api;

namespace WebMarket.Pipeline.CategoryLogic.Process.Delete
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

            if ((parameters.CategoryId == 0) && string.IsNullOrEmpty(parameters.Name))
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Category name not valid", "");
                return Task.FromResult(parameters);
            }

            if (parameters.CategoryId <= 0)
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Category id not valid", "");
                return Task.FromResult(parameters);
            }

            logger.LogDebug("Category validated");

            return Task.FromResult(parameters);
        }
    }
}
