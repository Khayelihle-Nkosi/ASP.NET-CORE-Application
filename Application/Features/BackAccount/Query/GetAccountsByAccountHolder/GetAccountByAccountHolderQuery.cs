using Application.Common.Interfaces;
using Application.Features.BackAccount.Query.GetAccountByAccountNumber;
using Domain.Entities.BankAccount;
using MediatR;

namespace Application.Features.BackAccount.Query.GetAccountsByAccountHolder;

public class GetAccountByAccountHolderQuery : IRequest<IEnumerable<BankAccountDto>> {
	public long AccountHolderId { get; }
	
	public GetAccountByAccountHolderQuery(long accountHolderId) {
		AccountHolderId = accountHolderId;
	}
}

public class GetAccountByAccountHolderQueryHandler : IRequestHandler<GetAccountByAccountHolderQuery, IEnumerable<BankAccountDto>> {
	private readonly IAccountService _accountService;

	public GetAccountByAccountHolderQueryHandler(IAccountService accountService) {
		_accountService = accountService;
	}

	public Task<IEnumerable<BankAccountDto>> Handle(GetAccountByAccountHolderQuery request, CancellationToken cancellationToken) {
		return Task.FromResult(_accountService.GetAccountsByAccountHolder(request.AccountHolderId));
	}
}