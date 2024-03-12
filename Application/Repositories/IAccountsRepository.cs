using Domain.Entities;
using Domain.Entities.BankAccount;
using Domain.Entities.Withdrawal;

namespace Application.Repositories;

public interface IAccountsRepository {
	Task<IEnumerable<BankAccount>> GetAccountsByAccountHolder(long id);
	Task<BankAccount?> GetAccountByAccountNumber(long accNumber);
	Task<bool> DebitAccount(BankAccount bankAccount);
	Task<bool> SaveWithdrawal(Withdrawal withdrawal);
}