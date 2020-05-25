using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Data;
using WebMarket.Model.Api;
using WebMarket.Model.Dto;
using WebMarket.Pipeline.CartItemLogic.Process;
using WebMarket.Pipeline.CartItemLogic.Process.Update;

namespace WebMarket.Pipeline.CartItemLogic
{
    [ExcludeFromCodeCoverage]
    public class UpdateCartItemPipeline : LogicPipeline<CartItemParameters>, IUpdateCartItemPipeline
    {
        private readonly ILogger<CartItemParameters> logger;

        public UpdateCartItemPipeline(ILogger<CartItemParameters> logger, IMapper mapper, MarketContext marketContext) : base(logger)
        {
            this.logger = logger;
            Add(new MapToDomainProcess(logger, mapper));
            Add(new ValidateProcess(logger));
            Add(new GetMarketEntityProcess(logger, marketContext));
            Add(new MapOrderProductProcess(logger));
            Add(new CommitProcess(logger, marketContext));
            Add(new MapFromOrderProductProcess(logger));
            Add(new MapDtoProcess(logger, mapper));
        }

        public async Task<ApiResponse> ExecutePipeline(CartItemDto model)
        {
            var parameters = new CartItemParameters()
            {
                ModelDto = model
            };
            try
            {
                parameters = await Execute(parameters);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new ApiResponse(HttpStatusCode.InternalServerError, $"There was a problem while processing pipeline {typeof(AddCartItemPipeline)}", "");
            }
            return parameters.Response;
        }
    }
}
