using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Data;
using WebMarket.Model.Api;

namespace WebMarket.Pipeline.ItemLogic.Process.Delete
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
            parameters.Product = marketContext.Product.Remove(parameters.Product).Entity;

            await marketContext.SaveChangesAsync();

            parameters.Response = new ApiResponse(HttpStatusCode.OK, "Item successfully deleted", "");

            logger.LogDebug("Item deleted and comitted");

            return parameters;
        }
    }
}
