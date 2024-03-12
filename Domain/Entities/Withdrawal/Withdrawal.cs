using Domain.Enums;

namespace Domain.Entities.Withdrawal;

public class Withdrawal {
	public Guid WithdrawalId { get; set; }
	public long WithdrawalAmount { get; set; }
	public DateTime Timestamp { get; set; }
	public long AccountNumber { get; set; }
	public AccountType AccountType { get; set; }
}