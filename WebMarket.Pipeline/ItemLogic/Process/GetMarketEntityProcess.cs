using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebMarket.Data;
using System.Linq;
using WebMarket.Model.Api;
using System.Net;
using WebMarket.Model.Data;
using System.Collections.Generic;

namespace WebMarket.Pipeline.ItemLogic.Process
{
    public class GetMarketEntityProcess : IProcess<ItemParameters>
    {
        private readonly ILogger<ItemParameters> logger;
        private readonly MarketContext marketContext;

        public GetMarketEntityProcess(ILogger<ItemParameters> logger, MarketContext marketContext)
        {
            this.logger = logger;
            this.marketContext = marketContext;
        }

        public async Task<ItemParameters> ExecuteAsync(ItemParameters parameters)
        {
            Task<List<Category>> taskCategoriesId = marketContext.Category.Where(c => parameters.Model.CategoryIds.Contains(c.CategoryId)).ToListAsync();
            Task<List<Category>> taskCategoriesName = marketContext.Category.Where(c => parameters.Model.CategoryNames.Contains(c.Name)).ToListAsync();
            Task<Product> taskProduct = marketContext.Product.FirstOrDefaultAsync(p => p.Name == parameters.Model.Name);

            await Task.WhenAll(taskCategoriesId, taskCategoriesName, taskProduct);

            parameters.Categories = await taskCategoriesId;
            List<Category> categoriesName = await taskCategoriesName;
            Product duplicate = await taskProduct;


            if (duplicate != null)
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Name already exists", "");
                return parameters;
            }

            if (parameters.Categories.Count != parameters.Model.CategoryIds.Count)
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Invalid Category Ids", "");
                return parameters;
            }

            if (categoriesName.Count != parameters.Model.CategoryNames.Count)
            {
                parameters.Abort = true;
                parameters.Response = new ApiResponse(HttpStatusCode.BadRequest, "Invalid Category Names", "");
                return parameters;
            }

            logger.LogDebug("Retrieve market models");

            return parameters;
        }
    }
}
