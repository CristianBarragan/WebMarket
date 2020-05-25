using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebMarket.Data;

namespace WebMarket.Pipeline.CartItemLogic.Process.Update
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
            parameters.OrderProduct = marketContext.OrderProduct.Update(parameters.OrderProduct).Entity;

            marketContext.Product.Update(parameters.OrderProduct.Product);

            await marketContext.SaveChangesAsync();

            logger.LogDebug("Cart Item updated and committed");

            return parameters;
        }
    }
}
