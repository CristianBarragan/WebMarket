﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Data;
using WebMarket.Model.Api;
using WebMarket.Model.Domain;
using WebMarket.Pipeline.CartItemLogic.Process.Get;
using WebMarket.Pipeline.CartItemLogic.Process;

namespace WebMarket.Pipeline.CartItemLogic
{
    [ExcludeFromCodeCoverage]
    public class GetCartItemPipeline : LogicPipeline<CartItemParameters>, IGetCartItemPipeline
    {
        private readonly ILogger<CartItemParameters> logger;

        public GetCartItemPipeline(ILogger<CartItemParameters> logger, IMapper mapper, MarketContext marketContext) : base(logger)
        {
            this.logger = logger;
            Add(new Process.Get.ValidateProcess(logger));
            Add(new GetProcess(logger, marketContext));
            Add(new MapDtoProcess(logger, mapper));
        }

        public async Task<ApiResponse> ExecutePipeline(long orderId, long productId)
        {
            var parameters = new CartItemParameters()
            {
                Model = new CartItem { OrderId = orderId, ProductId = productId }
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
