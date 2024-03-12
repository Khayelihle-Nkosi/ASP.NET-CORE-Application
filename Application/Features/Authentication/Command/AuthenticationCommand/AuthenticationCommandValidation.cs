using FluentValidation;

namespace Application.Features.Authentication.Command.AuthenticationCommand;

public class AuthenticationCommandValidation  : AbstractValidator<AuthenticationCommand> {
	public AuthenticationCommandValidation() {
		RuleFor(ac => ac.AccountHolderId)
			.NotEmpty().WithMessage("Account holder Id is required.");
	}
}