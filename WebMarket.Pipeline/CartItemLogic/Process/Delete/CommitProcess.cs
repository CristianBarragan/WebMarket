using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Data;
using WebMarket.Model.Api;

namespace WebMarket.Pipeline.CartItemLogic.Process.Delete
{
    public class CommitProcess : IProcess<CartItemParameters>
    {
        private readonly ILogger<CartItemParameters> logger;
        private readonly MarketContext marketContext;

        public CommitProcess(ILogger<CartItemParameters> logger, MarketContext marketContext)
        {
            this.logger = logger;
            this.marketContext = marketContext;
        }

        public async Task<CartItemParameters> ExecuteAsync(CartItemParameters parameters)
        {
            parameters.OrderProduct = marketContext.OrderProduct.Remove(parameters.OrderProduct).Entity;

            parameters.OrderProduct.Product.Quantity = parameters.OrderProduct.Product.Quantity + parameters.OrderProduct.Quantity;

            marketContext.Product.Update(parameters.OrderProduct.Product);

            await marketContext.SaveChangesAsync();

            parameters.Response = new ApiResponse(HttpStatusCode.OK, "Cart item successfully deleted", "");

            logger.LogDebug("Cart Item deleted and comitted");

            return parameters;
        }
    }
}
