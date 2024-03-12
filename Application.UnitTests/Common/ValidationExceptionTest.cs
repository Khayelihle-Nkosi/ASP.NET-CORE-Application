using Application.Common.Exceptions;
using FluentAssertions;
using FluentValidation.Results;

namespace Application.UnitTests.Common;

public class ValidationExceptionTest {
	[Test]
	public void DefaultConstructorCreatesAnEmptyErrorDictionary()
	{
		var actual = new ValidationException().Errors;

		actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
	}
	
	[Test]
	public void SingleValidationFailureCreatesASingleElementErrorDictionary()
	{
		var failures = new List<ValidationFailure>
		{
			new ValidationFailure("WithdrawalAmount", "needs to be bigger than 0."),
		};

		var actual = new ValidationException(failures).Errors;

		actual.Keys.Should().BeEquivalentTo(new string[] { "WithdrawalAmount" });
		actual["WithdrawalAmount"].Should().BeEquivalentTo(new string[] { "needs to be bigger than 0." });
	}
	
	[Test]
	public void MultipleValidationFailureForMultiplePropertiesCreatesAMultipleElementErrorDictionaryEachWithMultipleValues()
	{
		var failures = new List<ValidationFailure>
		{
			new ValidationFailure("WithdrawalAmount", "Withdrawal amount needs to be bigger than 0."),
			new ValidationFailure("WithdrawalAmount", "Withdrawal amount is required."),
			new ValidationFailure("WithdrawalAmount", "Bank account number is required."),
			new ValidationFailure("BankAccountNumber", "Bank account number is required."),
			new ValidationFailure("AccountHolderId", "Account holder Id is required.")
		};

		var actual = new ValidationException(failures).Errors;

		actual.Keys.Should().BeEquivalentTo(new string[] { "WithdrawalAmount", "BankAccountNumber", "AccountHolderId" });

		actual["WithdrawalAmount"].Should().BeEquivalentTo(new string[]
		{
			"Withdrawal amount needs to be bigger than 0.",
			"Withdrawal amount is required.",
			"Bank account number is required."
		});

		actual["BankAccountNumber"].Should().BeEquivalentTo(new string[]
		{
			"Bank account number is required."
		});
		
		actual["AccountHolderId"].Should().BeEquivalentTo(new string[]
		{
			"Account holder Id is required."
		});
	}
}