using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Data;
using WebMarket.Model.Api;
using WebMarket.Pipeline.CategoryLogic.Process;
using WebMarket.Pipeline.CategoryLogic.Process.Get;

namespace WebMarket.Pipeline.CategoryLogic
{
    [ExcludeFromCodeCoverage]
    public class GetCategoryPipeline : LogicPipeline<CategoryParameters>, IGetCategoryPipeline
    {
        private readonly ILogger<CategoryParameters> logger;

        public GetCategoryPipeline(ILogger<CategoryParameters> logger, IMapper mapper, MarketContext marketContext) : base(logger)
        {
            this.logger = logger;
            Add(new Process.Get.ValidateProcess(logger));
            Add(new GetProcess(logger, marketContext));
            Add(new MapDtoProcess(logger, mapper));
        }

        public async Task<ApiResponse> ExecutePipeline(long categoryId)
        {
            var parameters = new CategoryParameters()
            {
                CategoryId = categoryId
            };
            try
            {
                parameters = await Execute(parameters);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new ApiResponse(HttpStatusCode.InternalServerError, $"There was a problem while processing pipeline {typeof(GetCategoryPipeline)}", "");
            }
            return parameters.Response;
        }

        public async Task<ApiResponse> ExecutePipeline(string name)
        {
            var parameters = new CategoryParameters()
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
                return new ApiResponse(HttpStatusCode.InternalServerError, $"There was a problem while processing pipeline {typeof(GetCategoryPipeline)}", "");
            }
            return parameters.Response;
        }
    }
}
