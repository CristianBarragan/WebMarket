using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebMarket.Data;
using WebMarket.Model.Api;
using System.Net;
using WebMarket.Model.Data;

namespace WebMarket.Pipeline.CartItemLogic.Process.Add
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
            Task<Order> order = marketContext.Order.FirstOrDefaultAsync(o => o.OrderId == parameters.Model.OrderId);
            Task<Product> product = marketContext.Product.FirstOrDefaultAsync(o => o.ProductId == parameters.Model.ProductId);

            await Task.WhenAll(order, product);

            parameters.Order = await order;
            parameters.Product = await product;

            if (parameters.Order == null)
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Order Id not valid", "");
                return parameters;
            }

            if (parameters.Product == null)
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Product id not valid", "");
                return parameters;
            }

            logger.LogDebug("Retrieve market models");

            return parameters;
        }
    }
}
