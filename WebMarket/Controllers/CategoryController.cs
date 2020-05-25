using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMarket.Model.Api;
using WebMarket.Model.Dto;
using WebMarket.Pipeline.CategoryLogic;

namespace WebMarket.Controllers
{
    [ExcludeFromCodeCoverage]
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IAddCategoryPipeline _addCategory;
        private readonly IGetCategoryPipeline _getCategory;
        private readonly IGetAllCategoryPipeline _getAllCategory;
        private readonly IDeleteCategoryPipeline _deleteCategory;
        private readonly IUpdateCategoryPipeline _updateCategory;

        public CategoryController(IAddCategoryPipeline addCategory, IGetCategoryPipeline getCategory, IGetAllCategoryPipeline getAllCategory,
            IDeleteCategoryPipeline deleteCategory, IUpdateCategoryPipeline updateCategory)
        {
            _addCategory = addCategory;
            _getCategory = getCategory;
            _getAllCategory = getAllCategory;
            _deleteCategory = deleteCategory;
            _updateCategory = updateCategory;
        }

        // GET: api/Category
        [HttpGet("{pageSize}/{page}")]
        public async Task<IActionResult> GetAll(int pageSize, int page)
        {
            var apiResponse = await _getAllCategory.ExecutePipeline(pageSize, page);
            return new ApiActionResult(apiResponse);
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var apiResponse = await _getCategory.ExecutePipeline(id);
            return new ApiActionResult(apiResponse);
        }

        // POST: api/Category
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryDto Category)
        {
            if (!ModelState.IsValid)
            {
                return new ApiActionResult(new ApiResponse(HttpStatusCode.BadRequest, "Invalid model"));
            }
            var apiResponse = await _addCategory.ExecutePipeline(Category);
            return new ApiActionResult(apiResponse);
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] CategoryDto Category)
        {
            var apiResponse = await _updateCategory.ExecutePipeline(Category);
            return new ApiActionResult(apiResponse);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return new ApiActionResult(new ApiResponse(HttpStatusCode.BadRequest, "Invalid model"));
            }
            var apiResponse = await _deleteCategory.ExecutePipeline(id);
            return new ApiActionResult(apiResponse);
        }
    }
}
