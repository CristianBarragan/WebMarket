using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Data;
using WebMarket.Model.Api;

namespace WebMarket.Pipeline.CategoryLogic.Process.Delete
{
    public class CommitProcess : IProcess<CategoryParameters>
    {
        private readonly ILogger<CategoryParameters> logger;
        private readonly MarketContext marketContext;

        public CommitProcess(ILogger<CategoryParameters> logger, MarketContext marketContext)
        {
            this.logger = logger;
            this.marketContext = marketContext;
        }

        public async Task<CategoryParameters> ExecuteAsync(CategoryParameters parameters)
        {
            parameters.Model = marketContext.Category.Remove(parameters.Model).Entity;

            await marketContext.SaveChangesAsync();

            parameters.Response = new ApiResponse(HttpStatusCode.OK, "Category successfully deleted", "");

            logger.LogDebug("Category deleted and comitted");

            return parameters;
        }
    }
}
