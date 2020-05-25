using System.Threading.Tasks;
using WebMarket.Model.Api;

namespace WebMarket.Pipeline.ItemLogic
{
    public interface IDeleteItemPipeline
    {
        Task<ApiResponse> ExecutePipeline(long ItemId);

        Task<ApiResponse> ExecutePipeline(string name);
    }
}
