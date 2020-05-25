using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Data;
using WebMarket.Model.Api;
using WebMarket.Model.Dto;
using WebMarket.Pipeline.CategoryLogic.Process;
using WebMarket.Pipeline.CategoryLogic.Process.Update;

namespace WebMarket.Pipeline.CategoryLogic
{
    [ExcludeFromCodeCoverage]
    public class UpdateCategoryPipeline : LogicPipeline<CategoryParameters>, IUpdateCategoryPipeline
    {
        private readonly ILogger<CategoryParameters> logger;

        public UpdateCategoryPipeline(ILogger<CategoryParameters> logger, IMapper mapper, MarketContext marketContext) : base(logger)
        {
            this.logger = logger;
            Add(new MapToDomainProcess(logger, mapper));
            Add(new ValidateProcess(logger));
            Add(new GetMarketEntityProcess(logger, marketContext));
            Add(new CommitProcess(logger, marketContext));
            Add(new MapDtoProcess(logger, mapper));
        }

        public async Task<ApiResponse> ExecutePipeline(CategoryDto model)
        {
            var parameters = new CategoryParameters()
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
                return new ApiResponse(HttpStatusCode.InternalServerError, $"There was a problem while processing pipeline {typeof(UpdateCategoryPipeline)}", "");
            }
            return parameters.Response;
        }
    }
}
