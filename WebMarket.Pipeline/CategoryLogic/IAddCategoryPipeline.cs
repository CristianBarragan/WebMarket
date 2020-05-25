using System.Threading.Tasks;
using WebMarket.Model.Api;
using WebMarket.Model.Dto;

namespace WebMarket.Pipeline.CategoryLogic
{
    public interface IAddCategoryPipeline
    {
        Task<ApiResponse> ExecutePipeline(CategoryDto model);
    }
}
