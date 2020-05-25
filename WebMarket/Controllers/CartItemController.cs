using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMarket.Model.Api;
using WebMarket.Model.Dto;
using WebMarket.Pipeline.CartItemLogic;

namespace WebMarket.Controllers
{
    [ExcludeFromCodeCoverage]
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CartItemController : ControllerBase
    {
        private readonly IAddCartItemPipeline _addCartItem;
        private readonly IGetCartItemPipeline _getCartItem;
        private readonly IGetAllCartItemPipeline _getAllCartItem;
        private readonly IDeleteCartItemPipeline _deleteCartItem;
        private readonly IUpdateCartItemPipeline _updateCartItem;

        public CartItemController(IAddCartItemPipeline addCartItem, IGetCartItemPipeline getCartItem, IGetAllCartItemPipeline getAllCartItem,
            IDeleteCartItemPipeline deleteCartItem, IUpdateCartItemPipeline updateCartItem)
        {
            _addCartItem = addCartItem;
            _getCartItem = getCartItem;
            _getAllCartItem = getAllCartItem;
            _deleteCartItem = deleteCartItem;
            _updateCartItem = updateCartItem;
        }

        // GET: api/CartItem
        [HttpGet("{orderId}/{pageSize}/{page}")]
        public async Task<IActionResult> GetAll(long orderId, int pageSize, int page)
        {
            var apiResponse = await _getAllCartItem.ExecutePipeline(orderId, pageSize, page);
            return new ApiActionResult(apiResponse);
        }

        // GET: api/CartItem/5
        [HttpGet("{orderId}/{productId}")]
        public async Task<IActionResult> Get(long orderId, long productId)
        {
            var apiResponse = await _getCartItem.ExecutePipeline(orderId, productId);
            return new ApiActionResult(apiResponse);
        }

        // POST: api/CartItem
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CartItemDto CartItem)
        {
            if (!ModelState.IsValid)
            {
                return new ApiActionResult(new ApiResponse(HttpStatusCode.BadRequest, "Invalid model"));
            }
            var apiResponse = await _addCartItem.ExecutePipeline(CartItem);
            return new ApiActionResult(apiResponse);
        }

        // PUT: api/CartItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] CartItemDto CartItem)
        {
            var apiResponse = await _updateCartItem.ExecutePipeline(CartItem);
            return new ApiActionResult(apiResponse);
        }

        // DELETE: api/CartItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long orderId, long productId)
        {
            var apiResponse = await _deleteCartItem.ExecutePipeline(orderId, productId);
            return new ApiActionResult(apiResponse);
        }
    }
}
