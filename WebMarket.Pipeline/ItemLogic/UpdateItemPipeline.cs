﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Data;
using WebMarket.Model.Api;
using WebMarket.Model.Dto;
using WebMarket.Pipeline.ItemLogic.Process;
using WebMarket.Pipeline.ItemLogic.Process.Update;

namespace WebMarket.Pipeline.ItemLogic
{
    [ExcludeFromCodeCoverage]
    public class UpdateItemPipeline : LogicPipeline<ItemParameters>, IUpdateItemPipeline
    {
        private readonly ILogger<ItemParameters> logger;

        public UpdateItemPipeline(ILogger<ItemParameters> logger, IMapper mapper, MarketContext marketContext) : base(logger)
        {
            this.logger = logger;
            Add(new MapToDomainProcess(logger, mapper));
            Add(new ValidateProcess(logger));
            Add(new GetMarketEntityProcess(logger, marketContext));
            Add(new CommitProcess(logger, marketContext));
            Add(new MapFromProductProcess(logger));
            Add(new MapDtoProcess(logger, mapper));
        }

        public async Task<ApiResponse> ExecutePipeline(ItemDto model)
        {
            var parameters = new ItemParameters()
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
                return new ApiResponse(HttpStatusCode.InternalServerError, $"There was a problem while processing pipeline {typeof(UpdateItemPipeline)}", "");
            }
            return parameters.Response;
        }
    }
}
