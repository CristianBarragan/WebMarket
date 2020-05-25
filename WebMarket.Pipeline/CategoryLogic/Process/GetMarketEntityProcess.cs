using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebMarket.Data;
using WebMarket.Model.Api;
using System.Net;
using WebMarket.Model.Data;

namespace WebMarket.Pipeline.CategoryLogic.Process
{
    public class GetMarketEntityProcess : IProcess<CategoryParameters>
    {
        private readonly ILogger<CategoryParameters> logger;
        private readonly MarketContext marketContext;

        public GetMarketEntityProcess(ILogger<CategoryParameters> logger, MarketContext marketContext)
        {
            this.logger = logger;
            this.marketContext = marketContext;
        }

        public async Task<CategoryParameters> ExecuteAsync(CategoryParameters parameters)
        {
            Category duplicate = await marketContext.Category.FirstOrDefaultAsync(c => c.Name == parameters.Model.Name);

            if (duplicate != null)
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Name already exists", "");
                return parameters;
            }

            logger.LogDebug("Retrieve market models");

            return parameters;
        }
    }
}
