using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebMarket.Model.Domain;
using WebMarket.Model.Dto;

namespace WebMarket.Pipeline.ItemLogic.Process
{
    public class MapToDomainProcess : IProcess<ItemParameters>
    {
        private readonly ILogger<ItemParameters> logger;
        private readonly IMapper mapper;

        public MapToDomainProcess(ILogger<ItemParameters> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }

        public Task<ItemParameters> ExecuteAsync(ItemParameters parameters)
        {
            parameters.Model = mapper.Map<ItemDto, Item>(parameters.ModelDto);
            logger.LogDebug("Item Dto mapped");
            return Task.FromResult(parameters);
        }
    }
}
