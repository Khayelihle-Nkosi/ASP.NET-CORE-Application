using Application.Common.Interfaces;
using Domain.Entities.BankAccount;
using MediatR;

namespace Application.Features.BackAccount.Query.GetAccountByAccountNumber;

public class GetAccountByAccountNumberQuery : IRequest<BankAccountDto> {
	public long BankAccountNumber { get;}

	public GetAccountByAccountNumberQuery(long bankAccountNumber) {
		BankAccountNumber = bankAccountNumber;
	}
}

public class GetAccountByAccountNumberQueryHandler : IRequestHandler<GetAccountByAccountNumberQuery, BankAccountDto> {
	private readonly IAccountService _accountService;

	public GetAccountByAccountNumberQueryHandler(IAccountService accountService) {
		_accountService = accountService;
	}
	
	public Task<BankAccountDto> Handle(GetAccountByAccountNumberQuery request, CancellationToken cancellationToken) {
		return Task.FromResult(_accountService.GetAccountByAccountNumber(request.BankAccountNumber));
	}
}