using System.Threading.Tasks;
using WebMarket.Model.Api;
using WebMarket.Model.Dto;

namespace WebMarket.Pipeline.ItemLogic
{
    public interface IUpdateItemPipeline
    {
        Task<ApiResponse> ExecutePipeline(ItemDto model);
    }
}
