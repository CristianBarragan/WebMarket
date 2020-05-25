using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMarket.Model.Data;

namespace WebMarket.Pipeline.ItemLogic.Process.Add
{
    public class MapEntitiesProcess : IProcess<ItemParameters>
    {
        private readonly ILogger<ItemParameters> logger;

        public MapEntitiesProcess(ILogger<ItemParameters> logger)
        {
            this.logger = logger;
        }

        public Task<ItemParameters> ExecuteAsync(ItemParameters parameters)
        {

            Product p = new Product();
            p.Name = parameters.Model.Name;
            p.Quantity = parameters.Model.Quantity;
            p.Price = parameters.Model.Price;
            p.ProductCategory = new List<ProductCategory>();

            foreach (var category in parameters.Categories)
            {
                ProductCategory pc = new ProductCategory();
                pc.ProductId = parameters.Product.ProductId;
                pc.Product = parameters.Product;
                pc.CategoryId = category.CategoryId;
                pc.Category = category;
                p.ProductCategory.Add(pc);
            }

            logger.LogDebug("Product category mapped");

            return Task.FromResult(parameters);
        }
    }
}
