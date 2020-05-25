using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Model.Api;
using WebMarket.Model.Data;

namespace WebMarket.Pipeline.CartItemLogic.Process.Add
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
            op.SubTotal = op.Product.Price * parameters.Model.Quantity;
            
            //Modified Product inventory
            op.Product.Quantity = op.Product.Quantity - parameters.Model.Quantity;
            
            if (op.Product.Quantity < 0)
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Product quantity not available", "");
                return Task.FromResult(parameters);
            }

            parameters.OrderProduct = op;

            logger.LogDebug("Cart Item calculated");
            return Task.FromResult(parameters);
        }
    }
}
