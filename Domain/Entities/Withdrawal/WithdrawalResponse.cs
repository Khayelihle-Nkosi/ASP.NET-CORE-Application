using Domain.Enums;

namespace Domain.Entities.Withdrawal;

public class WithdrawalResponse {
	public long WithdrawalAmount { get; set; }
	public long RemainingBalance { get; set; }
	public DateTime Timestamp { get; set; }
	public long AccountNumber { get; set; }
	public AccountType AccountType { get; set; }
}