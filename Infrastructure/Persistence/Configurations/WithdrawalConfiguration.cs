using Domain.Entities.Withdrawal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class WithdrawalConfiguration : IEntityTypeConfiguration<Withdrawal> {
	public void Configure(EntityTypeBuilder<Withdrawal> builder) {
		builder.HasKey(w => w.WithdrawalId);
		builder.HasIndex(w => w.WithdrawalId);

		builder.Property(w => w.WithdrawalId)
		       .IsRequired()
		       .ValueGeneratedOnAdd();
		
		builder.Property(w => w.AccountNumber)
		       .IsRequired()
		       .HasMaxLength(25);
		
		builder.Property(w => w.AccountType)
		       .IsRequired()
		       .HasMaxLength(55);

		builder.Property(w => w.WithdrawalAmount)
		       .IsRequired();

		builder.Property(w => w.Timestamp)
		       .IsRequired()
		       .HasColumnType("datetime2");
	}
}