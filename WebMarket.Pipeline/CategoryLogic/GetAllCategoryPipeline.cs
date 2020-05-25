using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Data;
using WebMarket.Model.Api;
using WebMarket.Pipeline.CategoryLogic.Process.GetAll;

namespace WebMarket.Pipeline.CategoryLogic
{
    [ExcludeFromCodeCoverage]
    public class GetAllCategoryPipeline : LogicPipeline<CategoryParameters>, IGetAllCategoryPipeline
    {
        private readonly ILogger<CategoryParameters> logger;

        public GetAllCategoryPipeline(ILogger<CategoryParameters> logger, IMapper mapper, MarketContext marketContext) : base(logger)
        {
            this.logger = logger;
            Add(new ValidateProcess(logger));
            Add(new GetProcess(logger, marketContext));
            Add(new MapDtoProcess(logger, mapper));
        }

        public async Task<ApiResponse> ExecutePipeline(int pageSize, int page)
        {
            var parameters = new CategoryParameters()
            {
                PageSize = pageSize,
                Page = page
            };
            try
            {
                parameters = await Execute(parameters);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new ApiResponse(HttpStatusCode.InternalServerError, $"There was a problem while processing pipeline {typeof(GetAllCategoryPipeline)}", "");
            }
            return parameters.Response;
        }
    }
}
