using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Model.Api;
using WebMarket.Model.Domain;
using WebMarket.Model.Dto;

namespace WebMarket.Pipeline.ItemLogic.Process.GetAll
{
    public class MapDtoProcess : IProcess<ItemParameters>
    {
        private readonly ILogger<ItemParameters> logger;
        private readonly IMapper mapper;

        public MapDtoProcess(ILogger<ItemParameters> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }

        public Task<ItemParameters> ExecuteAsync(ItemParameters parameters)
        {
            parameters.Response = new ApiResponse(HttpStatusCode.OK, "" , mapper.Map<List<Item>, List<ItemDto>>(parameters.Models));
            logger.LogDebug("Items Dto mapped");
            return Task.FromResult(parameters);
        }
    }
}
