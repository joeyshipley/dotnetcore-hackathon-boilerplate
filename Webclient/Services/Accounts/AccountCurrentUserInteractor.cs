using System.Threading.Tasks;
using BOS.Webclient.Db.Accounts;
using BOS.Webclient.Infrastructure.Services;
using BOS.Webclient.Models.Accounts;

namespace BOS.Webclient.Services.Accounts
{
    public interface IAccountCurrentUserInteractor
        : IInteractor<LoggedInRequest, ApplicationUser> {}

    public class AccountCurrentUserInteractor : InteractorBase, IAccountCurrentUserInteractor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserRepository _userRepository;

        public AccountCurrentUserInteractor(
            IUnitOfWork unitOfWork,
            IApplicationUserRepository userRepository
        )
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<InteractorResult<ApplicationUser>> Call(LoggedInRequest request)
        {
            return await PerformCall(async () =>
            {
                ApplicationUser user = null;
                _unitOfWork.Worker(() => 
                {
                    user = _userRepository.FindComplete(request.UserId);
                });
                return InteractorResult<ApplicationUser>.ForSuccess(user);
            });
        }
    }
}