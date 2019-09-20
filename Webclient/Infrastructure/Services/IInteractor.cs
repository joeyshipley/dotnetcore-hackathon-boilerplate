using System.Threading.Tasks;

namespace BOS.Webclient.Infrastructure.Services
{
    public interface IInteractor<TRequest, TResult>
        where TResult : class
    {
        Task<InteractorResult<TResult>> Call(TRequest request);
    }

    public interface IInteractor<TRequest>
    {
        Task<VoidInteractorResult> Call(TRequest request);
    }

    public interface IInteractor
    {
        Task<VoidInteractorResult> Call();
    }
}