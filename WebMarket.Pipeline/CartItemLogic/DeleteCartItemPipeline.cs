using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Data;
using WebMarket.Model.Api;
using WebMarket.Pipeline.CartItemLogic.Process.Delete;

namespace WebMarket.Pipeline.CartItemLogic
{
    [ExcludeFromCodeCoverage]
    public class DeleteCartItemPipeline : LogicPipeline<CartItemParameters>, IDeleteCartItemPipeline
    {
        private readonly ILogger<CartItemParameters> logger;

        public DeleteCartItemPipeline(ILogger<CartItemParameters> logger, IMapper mapper, MarketContext marketContext) : base(logger)
        {
            this.logger = logger;
            Add(new ValidateProcess(logger));
            Add(new GetMarketEntityProcess(logger, marketContext));
            Add(new CommitProcess(logger, marketContext));
        }

        public async Task<ApiResponse> ExecutePipeline(long orderId, long productId)
        {
            var parameters = new CartItemParameters()
            {
                OrderId = orderId,
                ProductId = productId
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
