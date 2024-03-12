using Domain.Entities;
using Domain.Entities.AccountHolder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AccountHolderConfiguration : IEntityTypeConfiguration<AccountHolder> {
	public void Configure(EntityTypeBuilder<AccountHolder> builder) {
		
		builder.HasKey(holder => holder.IdNumber);

		builder.HasIndex(holder => holder.IdNumber);
		
		builder.Property(holder => holder.FirstName)
		       .HasMaxLength(55)
		       .IsRequired();
		
		builder.Property(holder => holder.LastName)
		       .HasMaxLength(55)
		       .IsRequired();
		
		builder.Property(holder => holder.MobileNumber)
		       .HasMaxLength(10)
		       .IsRequired();

		builder.Property(holder => holder.IdNumber)
		       .ValueGeneratedNever()
		       .HasMaxLength(13)
		       .IsRequired();

		builder.Property(holder => holder.Dob)
		       .IsRequired()
		       .HasColumnType("date");

		builder.Property(holder => holder.Email)
		       .HasMaxLength(256)
		       .IsRequired();
		
		builder.Property(holder => holder.ResidentialAddress)
		       .IsRequired();
	}
}