using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebMarket.Data;

namespace WebMarket.Pipeline.ItemLogic.Process.Update
{
    public class CommitProcess : IProcess<ItemParameters>
    {
        private readonly ILogger<ItemParameters> logger;
        private readonly MarketContext marketContext;

        public CommitProcess(ILogger<ItemParameters> logger, MarketContext marketContext)
        {
            this.logger = logger;
            this.marketContext = marketContext;
        }

        public async Task<ItemParameters> ExecuteAsync(ItemParameters parameters)
        {
            parameters.Product = marketContext.Product.Update(parameters.Product).Entity;

            await marketContext.SaveChangesAsync();

            logger.LogDebug("Item added and committed");

            return parameters;
        }
    }
}
