using Application.Common.Exceptions;
using Application.Repositories;
using Domain.Entities;
using Domain.Entities.BankAccount;
using Domain.Entities.Withdrawal;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class AccountRepository : IAccountsRepository {
	private readonly ApplicationDbContext _context;

	public AccountRepository(ApplicationDbContext context) {
		_context = context;
	}

	public Task<IEnumerable<BankAccount>> GetAccountsByAccountHolder(long id) {
		return Task.FromResult<IEnumerable<BankAccount>>(_context.BankAccounts.Where(acc => acc.AccountHolderId == id).ToList());
	}

	public Task<BankAccount?> GetAccountByAccountNumber(long accNumber) {
		return _context.BankAccounts.FirstOrDefaultAsync(acc => acc.AccountNumber == accNumber)!;
	}

	public async Task<bool> DebitAccount(BankAccount bankAccount) {
		var existingAccount = await _context.BankAccounts.FindAsync(bankAccount.AccountNumber);
		if (existingAccount is null) {
			throw new NotFoundException(nameof(BankAccount), bankAccount.AccountNumber);
		}
		try {
			_context.BankAccounts.Update(bankAccount);
			return await _context.SaveChangesAsync() > 0;
		}
		catch (DbUpdateConcurrencyException e) {
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<bool> SaveWithdrawal(Withdrawal withdrawal) {
		await _context.Withdrawals.AddAsync(withdrawal);
		return await _context.SaveChangesAsync() > 0;
	}
}