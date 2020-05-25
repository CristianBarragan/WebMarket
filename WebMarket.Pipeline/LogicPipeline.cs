using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebMarket.Model.Api;

namespace WebMarket.Pipeline
{
    public abstract class LogicPipeline<TObject>
        : List<IProcess<TObject>>
        where TObject : ILogicPipelineObject
    {
        private static ILogger<TObject> _logger;

        protected LogicPipeline(ILogger<TObject> logger)
        {
            _logger = logger;
        }

        public async Task<TObject> Execute(TObject parameters)
        {
            foreach (var process in this)
            {
                try
                {
                    if (!parameters.Abort)
                    {
                        parameters = await process.ExecuteAsync(parameters);
                    }
                    else
                    {
                        _logger.LogInformation(parameters.Response.Message);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    parameters.Response = new ApiResponse(HttpStatusCode.InternalServerError, $"There was a problem while processing pipeline {typeof(TObject)}", "");
                    return parameters;
                }
            }
            return parameters;
        }
    }
}
