namespace Domain.Entities.AccountHolder;

public class AccountHolder {
	public long IdNumber { get; set; }
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public DateTime Dob { get; set; }
	public string ResidentialAddress { get; set; } = null!;
	public long MobileNumber { get; set; }
	public string Email { get; set; } = null!;
}