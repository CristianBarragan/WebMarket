using System.Collections.Generic;
using WebMarket.Model.Api;

namespace WebMarket.Pipeline
{
    public interface ILogicPipelineObject
    {
        bool Abort { get; set; }

        ApiResponse Response { get; set; }

    }
}
