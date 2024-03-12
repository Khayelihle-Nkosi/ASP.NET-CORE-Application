namespace Domain.Common;

public class AuditTrail {
	public Guid Id { get; set; }
	public long UserId { get; set; }
	public string EntityName { get; set; } = null!;
	public string Action { get; set; } = null!;
	public DateTime Timestamp { get; set; }
	public string Changes { get; set; } = null!;
}