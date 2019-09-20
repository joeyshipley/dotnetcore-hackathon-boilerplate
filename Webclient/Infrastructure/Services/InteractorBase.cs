using System;
using System.Threading.Tasks;

namespace BOS.Webclient.Infrastructure.Services
{
    public class InteractorBase
    {
        protected async Task<InteractorResult<T>> PerformCall<T>(Func<Task<InteractorResult<T>>> performCall)
            where T : class
        {
            try
            {
                return await performCall();
            }
            catch (Exception exception)
            {
                return InteractorResult<T>.ForFailure(exception);
            }
        }

        protected async Task<VoidInteractorResult> PerformCall(Func<Task<VoidInteractorResult>> performCall)
        {
            try
            {
                return await performCall();
            }
            catch (Exception exception)
            {
                return VoidInteractorResult.ForFailure(exception);
            }
        }
    }
}