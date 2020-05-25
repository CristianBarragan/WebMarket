using System.Threading.Tasks;
using WebMarket.Model.Api;
using WebMarket.Model.Dto;

namespace WebMarket.Pipeline.CartItemLogic
{
    public interface IAddCartItemPipeline
    {
        Task<ApiResponse> ExecutePipeline(CartItemDto model);
    }
}
