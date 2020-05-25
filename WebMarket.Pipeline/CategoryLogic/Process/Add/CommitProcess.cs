using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebMarket.Data;

namespace WebMarket.Pipeline.CategoryLogic.Process.Add
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
            parameters.Model = marketContext.Category.Add(parameters.Model).Entity;

            await marketContext.SaveChangesAsync();

            logger.LogDebug("Category added and committed");

            return parameters;
        }
    }
}
