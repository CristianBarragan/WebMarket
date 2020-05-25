using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Model.Api;
using WebMarket.Model.Data;
using WebMarket.Model.Dto;

namespace WebMarket.Pipeline.CategoryLogic.Process
{
    public class MapDtoProcess : IProcess<CategoryParameters>
    {
        private readonly ILogger<CategoryParameters> logger;
        private readonly IMapper mapper;

        public MapDtoProcess(ILogger<CategoryParameters> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }

        public Task<CategoryParameters> ExecuteAsync(CategoryParameters parameters)
        {
            parameters.Response = new ApiResponse(HttpStatusCode.OK, "Category added", mapper.Map<Category, CategoryDto>(parameters.Model));
            logger.LogDebug("Category Dto mapped");
            return Task.FromResult(parameters);
        }
    }
}
