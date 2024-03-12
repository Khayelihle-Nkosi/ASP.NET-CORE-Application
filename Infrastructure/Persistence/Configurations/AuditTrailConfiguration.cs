using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AuditTrailConfiguration : IEntityTypeConfiguration<AuditTrail> {
	public void Configure(EntityTypeBuilder<AuditTrail> builder) {
		builder.HasKey(at => at.Id);
		
		builder.HasIndex(at => at.Id);
		
		builder.Property(at => at.Id)
		       .IsRequired()
		       .ValueGeneratedOnAdd();
		
		builder.Property(at => at.Timestamp)
		       .IsRequired()
		       .HasColumnType("datetime2");
		
		builder.Property(at => at.Action)
		       .IsRequired()
		       .HasMaxLength(256);
		
		builder.Property(at => at.EntityName)
		       .IsRequired()
		       .HasMaxLength(256);
		
		builder.Property(at => at.Changes)
		       .IsRequired()
		       .HasColumnType("nvarchar(max)");

		builder.Property(at => at.UserId)
		       .IsRequired()
		       .HasMaxLength(13);

	}
}