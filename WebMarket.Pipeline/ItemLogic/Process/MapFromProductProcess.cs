using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMarket.Model.Domain;

namespace WebMarket.Pipeline.ItemLogic.Process
{
    public class MapFromProductProcess : IProcess<ItemParameters>
    {
        private readonly ILogger<ItemParameters> logger;

        public MapFromProductProcess(ILogger<ItemParameters> logger)
        {
            this.logger = logger;
        }

        public Task<ItemParameters> ExecuteAsync(ItemParameters parameters)
        {
            parameters.Model = new Item();
            parameters.Model.ItemId = parameters.Product.ProductId;
            parameters.Model.Name = parameters.Product.Name;
            parameters.Model.Price = parameters.Product.Price;
            parameters.Model.Quantity = parameters.Product.Quantity;
            parameters.Model.CategoryNames = new List<string>();
            parameters.Model.CategoryIds = new List<long>();

            foreach (var productCategory in parameters.Product.ProductCategory)
            {
                parameters.Model.CategoryIds.Add(productCategory.Category.CategoryId);
                parameters.Model.CategoryNames.Add(productCategory.Category.Name);
            }

            logger.LogDebug("Map from Product to item");

            return Task.FromResult(parameters);
        }
    }
}
