using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebMarket.Data;
using WebMarket.Model.Api;
using System.Net;

namespace WebMarket.Pipeline.ItemLogic.Process.Delete
{
    public class GetMarketEntityProcess : IProcess<ItemParameters>
    {
        private readonly ILogger<ItemParameters> logger;
        private readonly MarketContext marketContext;

        public GetMarketEntityProcess(ILogger<ItemParameters> logger, MarketContext marketContext)
        {
            this.logger = logger;
            this.marketContext = marketContext;
        }

        public async Task<ItemParameters> ExecuteAsync(ItemParameters parameters)
        {
            if (parameters.ItemId != 0)
                parameters.Product = await marketContext.Product.FirstOrDefaultAsync(c => c.ProductId == parameters.ItemId);
            else
                parameters.Product = await marketContext.Product.FirstOrDefaultAsync(c => c.Name == parameters.Name);

            if (parameters.Product == null)
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Item Id not valid", "");
                return parameters;
            }

            logger.LogDebug("Retrieve market models");

            return parameters;
        }
    }
}
