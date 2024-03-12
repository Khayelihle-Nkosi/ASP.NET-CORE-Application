using Application.Features.BackAccount.Query.GetAccountByAccountNumber;
using Domain.Entities.BankAccount;
using Domain.Entities.Withdrawal;

namespace Application.Common.Interfaces;

public interface IAccountService {
	IEnumerable<BankAccountDto> GetAccountsByAccountHolder(long id);
	BankAccountDto GetAccountByAccountNumber(long accNumber);
	WithdrawalResponse CreateWithdrawal(long accountNumber, long withdrawalAmount);
}