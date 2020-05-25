using System.Threading.Tasks;
using WebMarket.Model.Api;

namespace WebMarket.Pipeline.CartItemLogic
{
    public interface IGetCartItemPipeline
    {
        Task<ApiResponse> ExecutePipeline(long OrderId, long ProductId);
    }
}
