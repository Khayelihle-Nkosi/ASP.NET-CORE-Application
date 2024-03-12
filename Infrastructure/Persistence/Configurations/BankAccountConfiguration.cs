using Domain.Entities;
using Domain.Entities.BankAccount;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount> {
	public void Configure(EntityTypeBuilder<BankAccount> builder) {

		builder.HasKey(ba => ba.AccountNumber);
		builder.HasIndex(ba => ba.AccountHolderId);
		builder.HasIndex(ba => ba.AccountNumber);
		
		builder.Property(ba => ba.AccountNumber)
		       .ValueGeneratedNever()
		       .HasMaxLength(25)
		       .IsRequired();
		
		builder.Property(ba => ba.AccountType)
		       .HasMaxLength(55)
		       .IsRequired();
		
		builder.Property(ba => ba.AccountName)
		       .HasMaxLength(256)
		       .IsRequired();
		
		builder.Property(ba => ba.AccountStatus)
		       .IsRequired();
		
		builder.Property(ba => ba.AvailableBalance)
		       .IsRequired()
		       .HasPrecision(int.MaxValue, 2);
		
		builder.Property(ba => ba.AccountHolderId)
		       .HasMaxLength(13)
		       .IsRequired();
	}
}