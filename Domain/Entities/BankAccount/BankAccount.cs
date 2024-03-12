using Domain.Enums;

namespace Domain.Entities.BankAccount;

public class BankAccount {
	public long AccountNumber { get; set; }
	public AccountType AccountType { get; set; }
	public string AccountName { get; set; } = null!;
	public bool AccountStatus { get; set; }
	public long AvailableBalance { get; set; }
	public long AccountHolderId { get; set; }
}