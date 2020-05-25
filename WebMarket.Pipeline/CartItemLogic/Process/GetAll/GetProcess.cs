using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebMarket.Data;
using System.Linq;
using WebMarket.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace WebMarket.Pipeline.CartItemLogic.Process.GetAll
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

        public async Task<CartItemParameters> ExecuteAsync(CartItemParameters parameters)
        {
            parameters.Models = await (from orderProduct in marketContext.OrderProduct
                                join order in marketContext.Order on orderProduct.OrderId equals order.OrderId
                                join product in marketContext.Product on orderProduct.ProductId equals product.ProductId
                                where orderProduct.OrderId == parameters.Model.OrderId
                                select new CartItem
                                {
                                    OrderId = orderProduct.OrderId,
                                    ProductId = orderProduct.ProductId,
                                    Name = product.Name,
                                    Quantity = orderProduct.Quantity,
                                    Total = orderProduct.SubTotal
                                }).Skip((parameters.Page - 1) * parameters.PageSize).Take(parameters.PageSize).ToListAsync();

            logger.LogDebug("Cart Items retrieved");

            return parameters;
        }
    }
}
