using System.Threading.Tasks;
using WebMarket.Model.Api;

namespace WebMarket.Pipeline.CartItemLogic
{
    public interface IGetAllCartItemPipeline
    {
        Task<ApiResponse> ExecutePipeline(long OrderId, int pageSize, int page);
    }
}
