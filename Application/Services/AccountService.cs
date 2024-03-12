using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings.AccountMapping;
using Application.Repositories;
using Domain.Entities.BankAccount;
using Domain.Entities.Withdrawal;
using Domain.Enums;
using FluentValidation.TestHelper;
using ValidationException = FluentValidation.ValidationException;

namespace Application.Services;

public class AccountService : IAccountService {
	private readonly IAccountsRepository _accountsRepository;

	public AccountService(IAccountsRepository accountsRepository) {
		_accountsRepository = accountsRepository;
	}

	public IEnumerable<BankAccountDto> GetAccountsByAccountHolder(long id) {
		var response = _accountsRepository.GetAccountsByAccountHolder(id).Result;
		if (!response.Any()) {
			throw new NotFoundException(nameof(BankAccountDto), id);
		}

		var accountMapper = new AccountMapping();
		return accountMapper.BankAccountMapping().Map<IEnumerable<BankAccountDto>>(response);
	}

	public BankAccountDto GetAccountByAccountNumber(long accNumber) {
		var response = _accountsRepository.GetAccountByAccountNumber(accNumber).Result;
		if (response is null) {
			throw new NotFoundException(nameof(BankAccountDto), accNumber);
		}

		var bankAccountMapper = new AccountMapping();
		return bankAccountMapper.BankAccountMapping().Map<BankAccountDto>(response);
	}

	public WithdrawalResponse CreateWithdrawal(long accountNumber, long withdrawalAmount) {
		var account = _accountsRepository.GetAccountByAccountNumber(accountNumber).Result;

		if (account is null) throw new NotFoundException(nameof(BankAccount), accountNumber);
		
		if (withdrawalAmount <= 0 && account.AccountType != AccountType.FixedDepositAccount) {
			throw new ValidationException("Withdrawal amount needs to be more than 0");
		}

		if (withdrawalAmount > account.AvailableBalance) {
			throw new ValidationException("Unable to withdraw more than the available balance");
		}

		if (account.AccountStatus) {
			WithdrawalResponse withdrawResponse;

			var withdrawal = new Withdrawal {
				AccountType = account.AccountType,
				AccountNumber = account.AccountNumber,
				WithdrawalAmount = withdrawalAmount,
				Timestamp = DateTime.UtcNow,
			};

			if (account.AccountType == AccountType.FixedDepositAccount) {
				account.AvailableBalance -= account.AvailableBalance;

				var savedWithdrawal = _accountsRepository.SaveWithdrawal(withdrawal).Result;

				if (savedWithdrawal) {
					var debited = _accountsRepository.DebitAccount(account).Result;

					if (!debited) throw new ValidationTestException("Failed to debit account.");
					withdrawResponse = new WithdrawalResponse {
						AccountType = account.AccountType,
						AccountNumber = account.AccountNumber,
						WithdrawalAmount = withdrawalAmount,
						Timestamp = withdrawal.Timestamp,
						RemainingBalance = account.AvailableBalance
					};
					return withdrawResponse;
				}
			}
			else {
				account.AvailableBalance -= withdrawalAmount;

				var savedWithdrawal = _accountsRepository.SaveWithdrawal(withdrawal).Result;

				if (savedWithdrawal) {
					var debited = _accountsRepository.DebitAccount(account).Result;
					if (!debited) throw new ValidationTestException("Failed to debit account.");
					withdrawResponse = new WithdrawalResponse {
						AccountType = account.AccountType,
						AccountNumber = account.AccountNumber,
						WithdrawalAmount = withdrawalAmount,
						Timestamp = withdrawal.Timestamp,
						RemainingBalance = account.AvailableBalance
					};
					return withdrawResponse;
				}
			}
		}
		throw new ValidationException("Unable to withdraw from an inactive account");

	}
}