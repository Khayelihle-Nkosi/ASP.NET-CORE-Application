namespace Domain.Entities.Withdrawal;

public class WithdrawRequest {
	public long AccountNumber { get; }
	public long WithdrawalAmount { get; }
	
	public WithdrawRequest(long withdrawalAmount, long accountNumber) {
		WithdrawalAmount = withdrawalAmount;
		AccountNumber = accountNumber;
	}
}