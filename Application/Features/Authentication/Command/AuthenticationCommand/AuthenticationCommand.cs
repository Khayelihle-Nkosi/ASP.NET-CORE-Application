using Application.Common.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Authentication.Command.AuthenticationCommand;

public class AuthenticationCommand : IRequest<string>{
	public long AccountHolderId { get; }

	public AuthenticationCommand(long accountHolderId) {
		AccountHolderId = accountHolderId;
	}
}

public class AuthenticationCommandHandler : IRequestHandler<AuthenticationCommand, string> {
	private readonly IAccountHolderService _accountHolderService;

	public AuthenticationCommandHandler(IAccountHolderService accountHolderService) {
		_accountHolderService = accountHolderService;
	}

	public Task<string> Handle(AuthenticationCommand request, CancellationToken cancellationToken) {
		
		return Task.FromResult(_accountHolderService.GetAccountHolder(request.AccountHolderId));
	}
}