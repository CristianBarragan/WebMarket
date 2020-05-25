using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Data;
using WebMarket.Model.Api;
using WebMarket.Pipeline.ItemLogic.Process.GetAll;

namespace WebMarket.Pipeline.ItemLogic
{
    [ExcludeFromCodeCoverage]
    public class GetAllItemPipeline : LogicPipeline<ItemParameters>, IGetAllItemPipeline
    {
        private readonly ILogger<ItemParameters> logger;

        public GetAllItemPipeline(ILogger<ItemParameters> logger, IMapper mapper, MarketContext marketContext) : base(logger)
        {
            this.logger = logger;
            Add(new ValidateProcess(logger ));
            Add(new GetProcess(logger, marketContext));
            Add(new MapDtoProcess(logger, mapper));
        }

        public async Task<ApiResponse> ExecutePipeline(int pageSize, int page)
        {
            var parameters = new ItemParameters()
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
                return new ApiResponse(HttpStatusCode.InternalServerError, $"There was a problem while processing pipeline {typeof(GetAllItemPipeline)}", "");
            }
            return parameters.Response;
        }
    }
}
