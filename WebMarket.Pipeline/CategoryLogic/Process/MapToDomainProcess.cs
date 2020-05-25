using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebMarket.Model.Data;
using WebMarket.Model.Dto;

namespace WebMarket.Pipeline.CategoryLogic.Process
{
    public class MapToDomainProcess : IProcess<CategoryParameters>
    {
        private readonly ILogger<CategoryParameters> logger;
        private readonly IMapper mapper;

        public MapToDomainProcess(ILogger<CategoryParameters> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }

        public Task<CategoryParameters> ExecuteAsync(CategoryParameters parameters)
        {
            parameters.Model = mapper.Map<CategoryDto, Category>(parameters.ModelDto);
            logger.LogDebug("Category Dto mapped");
            return Task.FromResult(parameters);
        }
    }
}
