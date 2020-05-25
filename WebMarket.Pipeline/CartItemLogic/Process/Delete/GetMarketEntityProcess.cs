using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebMarket.Data;
using WebMarket.Model.Api;
using System.Net;

namespace WebMarket.Pipeline.CartItemLogic.Process.Delete
{
    public class GetMarketEntityProcess : IProcess<CartItemParameters>
    {
        private readonly ILogger<CartItemParameters> logger;
        private readonly MarketContext marketContext;

        public GetMarketEntityProcess(ILogger<CartItemParameters> logger, MarketContext marketContext)
        {
            this.logger = logger;
            this.marketContext = marketContext;
        }

        public async Task<CartItemParameters> ExecuteAsync(CartItemParameters parameters)
        {

            parameters.OrderProduct = await marketContext.OrderProduct.FirstOrDefaultAsync(op => op.OrderId == parameters.Model.OrderId && op.ProductId == parameters.Model.ProductId);

            if (parameters.OrderProduct == null)
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Order product not valid", "");
                return parameters;
            }

            logger.LogDebug("Retrieve market models");

            return parameters;
        }
    }
}
