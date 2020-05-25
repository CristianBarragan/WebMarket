using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebMarket.Data;
using WebMarket.Model.Api;
using System.Net;

namespace WebMarket.Pipeline.CategoryLogic.Process.Delete
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
            if (parameters.CategoryId != 0)
                parameters.Model = await marketContext.Category.FirstOrDefaultAsync(c => c.CategoryId == parameters.CategoryId);
            else
                parameters.Model = await marketContext.Category.FirstOrDefaultAsync(c => c.Name == parameters.Name);

            if (parameters.Model == null)
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Category Id not valid", "");
                return parameters;
            }

            logger.LogDebug("Retrieve market models");

            return parameters;
        }
    }
}
