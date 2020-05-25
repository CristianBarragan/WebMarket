using System.Threading.Tasks;

namespace WebMarket.Pipeline
{
    public interface IProcess<TObject> where TObject : ILogicPipelineObject
    {
        Task<TObject> ExecuteAsync(TObject parameters);
    }
}
