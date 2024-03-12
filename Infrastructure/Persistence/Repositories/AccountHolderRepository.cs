using Application.Common.Exceptions;
using Application.Repositories;
using Domain.Entities;
using Domain.Entities.AccountHolder;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class AccountHolderRepository : IAccountHolderRepository {
	private readonly ApplicationDbContext _context;

	public AccountHolderRepository(ApplicationDbContext context) {
		_context = context;
	}

	public async Task<AccountHolder> GetAccountHolder(long accountHolderId) {
		var holder = await _context.AccountHolders.FirstOrDefaultAsync(accHolder => accHolder.IdNumber == accountHolderId);
	
		if (holder != null && holder.IdNumber.Equals(accountHolderId)) {
			return holder;
		}

		throw new NotFoundException(nameof(AccountHolder), accountHolderId);
	}
}