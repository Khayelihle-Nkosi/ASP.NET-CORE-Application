using System.Security.Claims;
using System.Text;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.AccountHolder;
using Domain.Entities.BankAccount;
using Domain.Entities.Withdrawal;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Persistence;
public class ApplicationDbContext : DbContext {
	private readonly IHttpContextAccessor _httpContextAccessor;
	public DbSet<BankAccount> BankAccounts => Set<BankAccount>();
	public DbSet<AccountHolder> AccountHolders => Set<AccountHolder>();
	public DbSet<Withdrawal> Withdrawals => Set<Withdrawal>();
	public DbSet<AuditTrail> AuditTrails => Set<AuditTrail>();

	public ApplicationDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options) {
		_httpContextAccessor = httpContextAccessor;
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
		modelBuilder.HasDefaultSchema("AssessmentSchema");
	}

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken()) {
		var modifiedEntities = ChangeTracker.Entries<BankAccount>()
		                                    .Where(e => e.State is EntityState.Modified)
		                                    .ToArray();

		foreach (var modifiedEntity in modifiedEntities) {
			var auditTrail = new AuditTrail {
				EntityName = modifiedEntity.Entity.GetType().Name,
				Action = modifiedEntity.State.ToString(),
				Timestamp = DateTime.UtcNow,
				Changes = GetChanges(modifiedEntity),
				UserId = Convert.ToInt64(_httpContextAccessor.HttpContext?.User.FindFirstValue("Id"))
			};

			await AuditTrails.AddAsync(auditTrail, cancellationToken);
		}
		return await base.SaveChangesAsync(cancellationToken);
	}

	private static string GetChanges(EntityEntry entity) {
		var changes = new StringBuilder();
		foreach (var property in entity.OriginalValues.Properties) {
			var originalValue = entity.OriginalValues[property];
			var currentValue = entity.CurrentValues[property];

			if (!Equals(originalValue, currentValue)) {
				changes.AppendLine($"{property.Name}: From '{originalValue}' to '{currentValue}'");
			}
		}
		
		return changes.ToString();
	}
}