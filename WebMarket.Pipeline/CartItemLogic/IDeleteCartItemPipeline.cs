using System.Threading.Tasks;
using WebMarket.Model.Api;

namespace WebMarket.Pipeline.CartItemLogic
{
    public interface IDeleteCartItemPipeline
    {
        Task<ApiResponse> ExecutePipeline(long orderId, long productId);
    }
}
