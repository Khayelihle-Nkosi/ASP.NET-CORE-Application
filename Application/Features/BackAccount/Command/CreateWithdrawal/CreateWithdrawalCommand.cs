using Application.Common.Interfaces;
using Domain.Entities.Withdrawal;
using MediatR;

namespace Application.Features.BackAccount.Command.CreateWithdrawal;

public class CreateWithdrawalCommand : IRequest<WithdrawalResponse> {
	public WithdrawRequest WithdrawRequest { get; }
	public CreateWithdrawalCommand(WithdrawRequest withdrawRequest) {
		WithdrawRequest = withdrawRequest;
	}
}

public class CreateWithdrawalCommandHandler : IRequestHandler<CreateWithdrawalCommand, WithdrawalResponse> {
	private readonly IAccountService _accountService;

	public CreateWithdrawalCommandHandler(IAccountService accountService) {
		_accountService = accountService;
	}

	public Task<WithdrawalResponse> Handle(CreateWithdrawalCommand request, CancellationToken cancellationToken) {
		return Task.FromResult(_accountService.CreateWithdrawal(request.WithdrawRequest.AccountNumber, request.WithdrawRequest.WithdrawalAmount));
	}
}