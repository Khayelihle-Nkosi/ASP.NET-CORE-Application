using FluentValidation;

namespace Application.Features.BackAccount.Command.CreateWithdrawal;

public class CreateWithdrawalCommandValidation : AbstractValidator<CreateWithdrawalCommand> {
	public CreateWithdrawalCommandValidation() {
		RuleFor(wc => wc.WithdrawRequest.AccountNumber)
			.NotEmpty().WithMessage("Bank account number is required.");
		
		RuleFor(wc => wc.WithdrawRequest.WithdrawalAmount)
			.NotEmpty().WithMessage("Withdrawal amount is required.")
			.GreaterThan(0).WithMessage("Withdrawal amount needs to be bigger than 0.");
	}
}