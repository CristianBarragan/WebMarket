using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebMarket.Data;
using Microsoft.EntityFrameworkCore;

namespace WebMarket.Pipeline.CategoryLogic.Process.Get
{
    public class GetProcess : IProcess<CategoryParameters>
    {
        private readonly ILogger<CategoryParameters> logger;
        private readonly MarketContext marketContext;

        public GetProcess(ILogger<CategoryParameters> logger, MarketContext marketContext)
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

            logger.LogDebug("Category retrieved");

            return parameters;
        }
    }
}
