using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebMarket.Model.Domain;
using WebMarket.Model.Dto;

namespace WebMarket.Pipeline.CartItemLogic.Process
{
    public class MapToDomainProcess : IProcess<CartItemParameters>
    {
        private readonly ILogger<CartItemParameters> logger;
        private readonly IMapper mapper;

        public MapToDomainProcess(ILogger<CartItemParameters> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }

        public Task<CartItemParameters> ExecuteAsync(CartItemParameters parameters)
        {
            parameters.Model = mapper.Map<CartItemDto, CartItem>(parameters.ModelDto);
            logger.LogDebug("Cart Item Dto mapped");
            return Task.FromResult(parameters);
        }
    }
}
