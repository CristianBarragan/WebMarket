using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebMarket.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebMarket.Model.Domain;

namespace WebMarket.Pipeline.ItemLogic.Process.Get
{
    public class GetProcess : IProcess<ItemParameters>
    {
        private readonly ILogger<ItemParameters> logger;
        private readonly MarketContext marketContext;

        public GetProcess(ILogger<ItemParameters> logger, MarketContext marketContext)
        {
            this.logger = logger;
            this.marketContext = marketContext;
        }

        public async Task<ItemParameters> ExecuteAsync(ItemParameters parameters)
        {
            if (parameters.ItemId != 0)
                parameters.Model = await (from productCategory in marketContext.ProductCategory
                                      join product in marketContext.Product on productCategory.ProductId equals product.ProductId
                                      join category in marketContext.Category on productCategory.CategoryId equals category.CategoryId
                                      where productCategory.ProductId == parameters.Model.ItemId
                                      group new { category.CategoryId, category.Name } by new { product.ProductId, product.Name, product.Price, product.Quantity }
                                      into g
                                      select new Item
                                      {
                                          ItemId = g.Key.ProductId,
                                          Name = g.Key.Name,
                                          Price = g.Key.Price,
                                          Quantity = g.Key.Quantity,
                                          CategoryIds = g.Select(x => x.CategoryId).ToList(),
                                          CategoryNames = g.Select(x => x.Name).ToList()
                                      }).FirstOrDefaultAsync();

            logger.LogDebug("Item retrieved");

            return parameters;
        }
    }
}
