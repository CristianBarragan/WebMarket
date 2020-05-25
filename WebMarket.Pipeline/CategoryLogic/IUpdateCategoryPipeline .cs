using System.Threading.Tasks;
using WebMarket.Model.Api;
using WebMarket.Model.Dto;

namespace WebMarket.Pipeline.CategoryLogic
{
    public interface IUpdateCategoryPipeline
    {
        Task<ApiResponse> ExecutePipeline(CategoryDto model);
    }
}
