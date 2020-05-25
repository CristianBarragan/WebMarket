using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebMarket.Data;

namespace WebMarket.Pipeline.ItemLogic.Process.Add
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
            parameters.Product = marketContext.Product.Add(parameters.Product).Entity;

            foreach (var productCategory in parameters.Product.ProductCategory)
            {
                marketContext.ProductCategory.Add(productCategory);
            }

            await marketContext.SaveChangesAsync();

            logger.LogDebug("Item added and committed");

            return parameters;
        }
    }
}
