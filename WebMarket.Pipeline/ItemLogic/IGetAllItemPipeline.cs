using System.Threading.Tasks;
using WebMarket.Model.Api;

namespace WebMarket.Pipeline.ItemLogic
{
    public interface IGetAllItemPipeline
    {
        Task<ApiResponse> ExecutePipeline(int pageSize, int page);
    }
}
