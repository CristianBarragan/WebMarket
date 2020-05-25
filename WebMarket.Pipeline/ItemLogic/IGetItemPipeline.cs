using System.Threading.Tasks;
using WebMarket.Model.Api;

namespace WebMarket.Pipeline.ItemLogic
{
    public interface IGetItemPipeline
    {
        Task<ApiResponse> ExecutePipeline(long ItemId);

        Task<ApiResponse> ExecutePipeline(string name);
    }
}
