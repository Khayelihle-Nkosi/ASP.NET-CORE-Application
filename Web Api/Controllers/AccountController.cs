using System.Net.Mime;
using Application.Features.Authentication.Command.AuthenticationCommand;
using Application.Features.BackAccount.Command.CreateWithdrawal;
using Application.Features.BackAccount.Query.GetAccountByAccountNumber;
using Application.Features.BackAccount.Query.GetAccountsByAccountHolder;
using Domain.Entities.BankAccount;
using Domain.Entities.Withdrawal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web_Api.Controllers;

public class AccountController: ApiControllerBase {
	
	[HttpGet("{accountNumber}")]
	public async Task<ActionResult<BankAccountDto>> GetAccountByAccountNumber(long accountNumber) {
		return await Mediator.Send(new GetAccountByAccountNumberQuery(accountNumber));
	}
	
	[HttpGet("Holder/{holderId}")]
	public async Task<ActionResult<IEnumerable<BankAccountDto>>> GetAccountsByHolderId(long holderId) {
		var results = await Mediator.Send(new GetAccountByAccountHolderQuery(holderId));
		return results.ToList();
	}

	[HttpPut("Withdrawal")]
	[Consumes(MediaTypeNames.Application.Json)]
	public async Task<ActionResult<WithdrawalResponse>> Withdrawal([FromBody] WithdrawRequest withdrawRequest) {
		var command = new CreateWithdrawalCommand(withdrawRequest);
		if (withdrawRequest.AccountNumber != command.WithdrawRequest.AccountNumber || withdrawRequest.WithdrawalAmount != command.WithdrawRequest.WithdrawalAmount) {
			return BadRequest();
		}
		return Ok(await Mediator.Send(command));
	}

	[AllowAnonymous]
	[HttpPost]
	[Produces(MediaTypeNames.Text.Plain)]
	[Route("Authenticate/{holderId}")]
	public async Task<ActionResult<string>> Authenticate(long holderId) {
		var command = new AuthenticationCommand(holderId);
		if (holderId != command.AccountHolderId) {
			return BadRequest();
		}
		return Ok($"Token: {await Mediator.Send(command)}");
	} 
}