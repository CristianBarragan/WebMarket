using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebMarket.Data;
using System.Linq;
using WebMarket.Model.Domain;

namespace WebMarket.Pipeline.CartItemLogic.Process.Get
{
    public class GetProcess : IProcess<CartItemParameters>
    {
        private readonly ILogger<CartItemParameters> logger;
        private readonly MarketContext marketContext;

        public GetProcess(ILogger<CartItemParameters> logger, MarketContext marketContext)
        {
            this.logger = logger;
            this.marketContext = marketContext;
        }

        public Task<CartItemParameters> ExecuteAsync(CartItemParameters parameters)
        {
            parameters.Model = (from orderProduct in marketContext.OrderProduct
                         join order in marketContext.Order on orderProduct.OrderId equals order.OrderId
                         join product in marketContext.Product on orderProduct.ProductId equals product.ProductId
                         where orderProduct.OrderId == parameters.Model.OrderId && orderProduct.ProductId == parameters.Model.ProductId
                         select new CartItem
                         {
                             OrderId = orderProduct.OrderId,
                             ProductId = orderProduct.ProductId,
                             Name = product.Name,
                             Quantity = orderProduct.Quantity,
                             Total = orderProduct.SubTotal
                         }).FirstOrDefault();

            logger.LogDebug("Cart Item set");

            return Task.FromResult(parameters);
        }
    }
}
