using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMarket.Model.Api;
using WebMarket.Model.Dto;
using WebMarket.Pipeline.ItemLogic;

namespace WebMarket.Controllers
{
    [ExcludeFromCodeCoverage]
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IAddItemPipeline _addItem;
        private readonly IGetItemPipeline _getItem;
        private readonly IGetAllItemPipeline _getAllItem;
        private readonly IDeleteItemPipeline _deleteItem;
        private readonly IUpdateItemPipeline _updateItem;

        public ItemController(IAddItemPipeline addItem, IGetItemPipeline getItem, IGetAllItemPipeline getAllItem,
            IDeleteItemPipeline deleteItem, IUpdateItemPipeline updateItem)
        {
            _addItem = addItem;
            _getItem = getItem;
            _getAllItem = getAllItem;
            _deleteItem = deleteItem;
            _updateItem = updateItem;
        }

        // GET: api/Item
        [HttpGet]
        [HttpGet("{pageSize}/{page}")]
        public async Task<IActionResult> GetAll(int pageSize, int page)
        {
            var apiResponse = await _getAllItem.ExecutePipeline(pageSize, page);
            return new ApiActionResult(apiResponse);
        }

        // GET: api/Item/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var apiResponse = await _getItem.ExecutePipeline(id);
            return new ApiActionResult(apiResponse);
        }

        // POST: api/Item
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ItemDto Item)
        {
            if (!ModelState.IsValid)
            {
                return new ApiActionResult(new ApiResponse(HttpStatusCode.BadRequest, "Invalid model"));
            }
            var apiResponse = await _addItem.ExecutePipeline(Item);
            return new ApiActionResult(apiResponse);
        }

        // PUT: api/Item/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] ItemDto Item)
        {
            var apiResponse = await _updateItem.ExecutePipeline(Item);
            return new ApiActionResult(apiResponse);
        }

        // DELETE: api/Item/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var apiResponse = await _deleteItem.ExecutePipeline(id);
            return new ApiActionResult(apiResponse);
        }
    }
}
