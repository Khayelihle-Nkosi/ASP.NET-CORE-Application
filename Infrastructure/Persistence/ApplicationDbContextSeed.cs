using Domain.Entities;
using Domain.Entities.AccountHolder;
using Domain.Entities.BankAccount;
using Domain.Enums;

namespace Infrastructure.Persistence;

public static class ApplicationDbContextSeed {
	public static async Task SeedSampleDataAsync(ApplicationDbContext context) {
		//Seed, if necessary
		if (!context.AccountHolders.Any()) {
			IEnumerable<AccountHolder> accountHolders = new List<AccountHolder> {
				new AccountHolder {
					IdNumber = 8509203210987,
					FirstName = "Jane",
					LastName = "Smith",
					Dob = new DateTime(1985, 9, 20),
					ResidentialAddress = "456 Oak St, City, Country",
					MobileNumber = 0861235879,
					Email = "jane.smith@example.com"
				},
				new AccountHolder {
					IdNumber = 8803157890123,
					FirstName = "Joe",
					LastName = "Doe",
					Dob = new DateTime(1988, 3, 15),
					ResidentialAddress = "12 Sam St, City, Country",
					MobileNumber = 0869876543,
					Email = "joe.doe@example.com"
				},
				new AccountHolder {
					IdNumber = 9312297777777,
					FirstName = "Mike",
					LastName = "Jones",
					Dob = new DateTime(1993, 12, 29),
					ResidentialAddress = "457 Hamburg Str, City, Country",
					MobileNumber = 0781234567,
					Email = "mike.jone@example.com"
				},
			};
			foreach (var holder in accountHolders) {
				context.AccountHolders.Add(holder);
			}
			await context.SaveChangesAsync();
		}

		if (!context.BankAccounts.Any()) {
			IEnumerable<BankAccount> bankAccounts = new List<BankAccount> {
				new BankAccount {
					AccountNumber = 123456789,
					AccountType = AccountType.ChequeAccount,
					AccountName = "Premium Cheque Account",
					AccountStatus = true,
					AvailableBalance = 5000,
					AccountHolderId = 8509203210987
				},
				new BankAccount {
					AccountNumber = 987654321,
					AccountType = AccountType.SavingsAccount,
					AccountName = "Life starter Savings Account",
					AccountStatus = true,
					AvailableBalance = 1000000,
					AccountHolderId = 8509203210987
				},
				new BankAccount {
					AccountNumber = 555555555,
					AccountType = AccountType.FixedDepositAccount,
					AccountName = "Fixed Deposit Account",
					AccountStatus = false,
					AvailableBalance = 350000,
					AccountHolderId = 8509203210987
				},
				new BankAccount {
					AccountNumber = 777777777,
					AccountType = AccountType.ChequeAccount,
					AccountName = "Silver Cheque Account",
					AccountStatus = true,
					AvailableBalance = -500,
					AccountHolderId = 8803157890123
				},
				new BankAccount {
					AccountNumber = 999999999,
					AccountType = AccountType.SavingsAccount,
					AccountName = "Silver Savings Account",
					AccountStatus = true,
					AvailableBalance = 7500,
					AccountHolderId = 8803157890123
				},
				new BankAccount {
					AccountNumber = 223311556,
					AccountType = AccountType.FixedDepositAccount,
					AccountName = "Premium Fixed Deposit Account",
					AccountStatus = true,
					AvailableBalance = 75000,
					AccountHolderId = 8803157890123
				}
			};
			foreach (var account in bankAccounts) {
				context.BankAccounts.Add(account);
			}
			await context.SaveChangesAsync();
		}
	}
}