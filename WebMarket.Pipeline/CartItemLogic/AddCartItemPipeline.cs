using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Data;
using WebMarket.Model.Api;
using WebMarket.Model.Dto;
using WebMarket.Pipeline.CartItemLogic.Process.Add;
using WebMarket.Pipeline.CartItemLogic.Process;

namespace WebMarket.Pipeline.CartItemLogic
{
    [ExcludeFromCodeCoverage]
    public class AddCartItemPipeline : LogicPipeline<CartItemParameters>, IAddCartItemPipeline
    {
        private readonly ILogger<CartItemParameters> logger;

        public AddCartItemPipeline(ILogger<CartItemParameters> logger, IMapper mapper, MarketContext marketContext) : base(logger)
        {
            this.logger = logger;
            Add(new MapToDomainProcess(logger, mapper));
            Add(new ValidateProcess(logger));
            Add(new Process.Add.GetMarketEntityProcess(logger, marketContext));
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
