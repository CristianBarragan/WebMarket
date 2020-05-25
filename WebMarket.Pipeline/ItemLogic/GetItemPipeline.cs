using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Data;
using WebMarket.Model.Api;
using WebMarket.Pipeline.ItemLogic.Process;

namespace WebMarket.Pipeline.ItemLogic
{
    [ExcludeFromCodeCoverage]
    public class GetItemPipeline : LogicPipeline<ItemParameters>, IGetItemPipeline
    {
        private readonly ILogger<ItemParameters> logger;

        public GetItemPipeline(ILogger<ItemParameters> logger, IMapper mapper, MarketContext marketContext) : base(logger)
        {
            this.logger = logger;
            Add(new Process.Get.ValidateProcess(logger));
            Add(new Process.Get.GetProcess(logger, marketContext));
            Add(new MapDtoProcess(logger, mapper));
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
                return new ApiResponse(HttpStatusCode.InternalServerError, $"There was a problem while processing pipeline {typeof(GetItemPipeline)}", "");
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
                return new ApiResponse(HttpStatusCode.InternalServerError, $"There was a problem while processing pipeline {typeof(GetItemPipeline)}", "");
            }
            return parameters.Response;
        }
    }
}
