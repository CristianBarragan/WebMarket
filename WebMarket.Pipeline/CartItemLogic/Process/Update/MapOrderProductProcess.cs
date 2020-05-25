using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Model.Api;
using WebMarket.Model.Data;

namespace WebMarket.Pipeline.CartItemLogic.Process.Update
{
    public class MapOrderProductProcess : IProcess<CartItemParameters>
    {
        private readonly ILogger<CartItemParameters> logger;

        public MapOrderProductProcess(ILogger<CartItemParameters> logger)
        {
            this.logger = logger;
        }

        public Task<CartItemParameters> ExecuteAsync(CartItemParameters parameters)
        {
            OrderProduct op = new OrderProduct();
            op.Order = parameters.Order;
            op.Product = parameters.Product;
            op.OrderId = parameters.Model.OrderId;
            op.ProductId = parameters.Model.ProductId;
            op.Quantity = parameters.Model.Quantity;
            if (parameters.OrderProduct.Quantity > parameters.Model.Quantity)
            {
                op.Product.Quantity = op.Product.Quantity + (parameters.OrderProduct.Quantity - parameters.Model.Quantity);
            }
            else
            {
                op.Product.Quantity = op.Product.Quantity - (parameters.Model.Quantity - parameters.OrderProduct.Quantity);
            }

            if (op.Product.Quantity < 0)
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Product quantity not available", "");
                return Task.FromResult(parameters);
            }

            op.SubTotal = op.Product.Price * op.Quantity;
            
            parameters.OrderProduct = op;

            logger.LogDebug("Cart Item calculated");
            return Task.FromResult(parameters);
        }
    }
}
