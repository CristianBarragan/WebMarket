using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebMarket.Model.Domain;

namespace WebMarket.Pipeline.CartItemLogic.Process
{
    public class MapFromOrderProductProcess : IProcess<CartItemParameters>
    {
        private readonly ILogger<CartItemParameters> logger;

        public MapFromOrderProductProcess(ILogger<CartItemParameters> logger)
        {
            this.logger = logger;
        }

        public Task<CartItemParameters> ExecuteAsync(CartItemParameters parameters)
        {
            parameters.Model = new CartItem();
            parameters.Model.OrderId = parameters.OrderProduct.OrderId;
            parameters.Model.ProductId = parameters.OrderProduct.ProductId;
            parameters.Model.Name = parameters.OrderProduct.Product.Name;
            parameters.Model.Quantity = parameters.OrderProduct.Quantity;
            parameters.Model.Total = parameters.OrderProduct.SubTotal;

            logger.LogDebug("From OrderProduct to Cart Item mapped");

            return Task.FromResult(parameters);
        }
    }
}
