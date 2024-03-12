using Domain.Entities;
using Domain.Entities.AccountHolder;

namespace Application.Repositories;

public interface IAccountHolderRepository {
	Task<AccountHolder> GetAccountHolder(long accountHolderId);
}