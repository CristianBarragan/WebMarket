using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Data;
using WebMarket.Model.Api;
using WebMarket.Pipeline.ItemLogic.Process.Delete;

namespace WebMarket.Pipeline.ItemLogic
{
    [ExcludeFromCodeCoverage]
    public class DeleteItemPipeline : LogicPipeline<ItemParameters>, IDeleteItemPipeline
    {
        private readonly ILogger<ItemParameters> logger;

        public DeleteItemPipeline(ILogger<ItemParameters> logger, MarketContext marketContext) : base(logger)
        {
            this.logger = logger;
            Add(new ValidateProcess(logger));
            Add(new GetMarketEntityProcess(logger, marketContext));
            Add(new CommitProcess(logger, marketContext));
        }

        public async Task<ApiResponse> ExecutePipeline(long ItemId)
        {
            var parameters = new ItemParameters()
            {
                ItemId = ItemId
            };
            try
            {
                parameters = await Execute(parameters);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new ApiResponse(HttpStatusCode.InternalServerError, $"There was a problem while processing pipeline {typeof(DeleteItemPipeline)}", "");
            }
            return parameters.Response;
        }

        public async Task<ApiResponse> ExecutePipeline(string name)
        {
            var parameters = new ItemParameters()
            {
                Name = name
            };
            try
            {
                parameters = await Execute(parameters);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new ApiResponse(HttpStatusCode.InternalServerError, $"There was a problem while processing pipeline {typeof(DeleteItemPipeline)}", "");
            }
            return parameters.Response;
        }
    }
}
