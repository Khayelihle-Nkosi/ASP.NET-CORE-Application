using FluentValidation;

namespace Application.Features.BackAccount.Query.GetAccountsByAccountHolder;

public class GetAccountByAccountHolderQueryValidator : AbstractValidator<GetAccountByAccountHolderQuery> {
	public GetAccountByAccountHolderQueryValidator() {
		RuleFor(aq => aq.AccountHolderId)
			.NotEmpty().WithMessage("Account holder Id is required.");
	}
}