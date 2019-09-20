using BOS.Webclient.Infrastructure.Models;
using FluentValidation;

namespace BOS.Webclient.Models.Accounts
{
    public class ApplicationUser : IdentityUserBase<ApplicationUser, ApplicationUserValidator>
    {
        public string Name { get; set; }
    }

    public class ApplicationUserValidator : AbstractValidator<ApplicationUser>
    {
        public const int NameMinLength = 3;
        public const int NameMaxLength = 30; 

        public ApplicationUserValidator()
        {
            RuleFor(x => x.Email)
                .NotNull().NotEmpty()
                .WithMessage($"Email is required");

            RuleFor(x => x.Name)
                .NotNull().NotEmpty()
                .Length(NameMinLength, NameMaxLength)
                .WithMessage($"Name is required and must be from { NameMinLength } to { NameMaxLength } characters.");
        }
    }
}