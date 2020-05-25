using System.Threading.Tasks;
using WebMarket.Model.Api;

namespace WebMarket.Pipeline.CategoryLogic
{
    public interface IGetAllCategoryPipeline
    {
        Task<ApiResponse> ExecutePipeline(int pageSize, int page);
    }
}
