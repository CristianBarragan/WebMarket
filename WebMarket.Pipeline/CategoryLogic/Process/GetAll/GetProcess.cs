using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebMarket.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebMarket.Pipeline.CategoryLogic.Process.GetAll
{
    public class GetProcess : IProcess<CategoryParameters>
    {
        private readonly ILogger<CategoryParameters> logger;
        private readonly MarketContext marketContext;

        public GetProcess(ILogger<CategoryParameters> logger, MarketContext marketContext)
        {
            this.logger = logger;
            this.marketContext = marketContext;
        }

        public async Task<CategoryParameters> ExecuteAsync(CategoryParameters parameters)
        {
            parameters.Models = await marketContext.Category.Skip((parameters.Page - 1) * parameters.PageSize).Take(parameters.PageSize).ToListAsync();

            logger.LogDebug("Categories retrieved");

            return parameters;
        }
    }
}
