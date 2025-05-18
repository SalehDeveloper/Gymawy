using ErrorOr;
using Gymawy.Application.Abstractions.Auth;
using Gymawy.Application.Abstractions.Stripe;
using Gymawy.Domain.Abstractions;
using Gymawy.Domain.Admins;
using Gymawy.Domain.Subscriptions;
using Gymawy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Profiles.Commands.CreateStripeAccount
{
    public class CreateStripeAccountCommandHandler : IRequestHandler<CreateStripeAccountCommand, ErrorOr<StripeAccountResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAdminsRepository _adminsRepository;
        private readonly IUserContext _userContext;
        private readonly IStripeService _stripeService;


        public CreateStripeAccountCommandHandler(
            IUnitOfWork unitOfWork,
            IAdminsRepository adminsRepository,
            IUserContext userContext,
            IStripeService stripeService)
        {
            _unitOfWork = unitOfWork;
            _adminsRepository = adminsRepository;
            _userContext = userContext;
            _stripeService = stripeService;
        }

        public async Task<ErrorOr<StripeAccountResponse>> Handle(CreateStripeAccountCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.UserId;

            var admin = await _adminsRepository.FindAsync(x=> x.UserId == user , new[] {nameof(Admin.User)} , cancellationToken);

            if (admin.SubscriptionId!=null)
            {

                var response = await _stripeService.CreateStripeAccountLinkForAdmin(admin.Id, admin.User.Email);

                var result = admin.SetStripeAccountId(response.accountID);


                if (result.IsError)
                    return result.Errors;

                await _unitOfWork.CompleteAsync();

                return response;
               
            }
            
         
            return SubscriptionErrors.NotFound;
             
        }
    }
}
