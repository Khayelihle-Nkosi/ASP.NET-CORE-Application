using FluentValidation;

namespace Application.Features.BackAccount.Query.GetAccountByAccountNumber;

public class GetAccountByAccountNumberQueryValidator : AbstractValidator<GetAccountByAccountNumberQuery> {
	public GetAccountByAccountNumberQueryValidator() {
		RuleFor(aq => aq.BankAccountNumber)
			.NotEmpty().WithMessage("Bank account number is required.");
	}
}