using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Data;
using WebMarket.Model.Api;
using WebMarket.Pipeline.CategoryLogic.Process.Delete;

namespace WebMarket.Pipeline.CategoryLogic
{
    [ExcludeFromCodeCoverage]
    public class DeleteCategoryPipeline : LogicPipeline<CategoryParameters>, IDeleteCategoryPipeline
    {
        private readonly ILogger<CategoryParameters> logger;

        public DeleteCategoryPipeline(ILogger<CategoryParameters> logger, MarketContext marketContext) : base(logger)
        {
            this.logger = logger;
            Add(new ValidateProcess(logger));
            Add(new GetMarketEntityProcess(logger, marketContext));
            Add(new CommitProcess(logger, marketContext));
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
                return new ApiResponse(HttpStatusCode.InternalServerError, $"There was a problem while processing pipeline {typeof(DeleteCategoryPipeline)}", "");
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
                return new ApiResponse(HttpStatusCode.InternalServerError, $"There was a problem while processing pipeline {typeof(DeleteCategoryPipeline)}", "");
            }
            return parameters.Response;
        }
    }
}
