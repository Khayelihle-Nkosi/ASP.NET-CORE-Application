using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Repositories;
using Domain.Entities.AccountHolder;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ValidationException = FluentValidation.ValidationException;

namespace Application.Services;

public class AccountHolderService : IAccountHolderService {
	private readonly IAccountHolderRepository _accountHolderRepository;
	private readonly IConfiguration _config;

	public AccountHolderService(IAccountHolderRepository accountHolderRepository, IConfiguration config) {
		_accountHolderRepository = accountHolderRepository;
		_config = config;
	}

	public string GetAccountHolder(long accountHolderId) {
		if (accountHolderId <= 0) throw new ValidationException("Account holder Id is invalid.");
		
		var loggedInUser = _accountHolderRepository.GetAccountHolder(accountHolderId).Result;
		
		if (loggedInUser is null) throw new NotFoundException(nameof(AccountHolder), accountHolderId);

		var claims = new[] {
			new Claim("FirstName", loggedInUser.FirstName),
			new Claim("LastName", loggedInUser.LastName),
			new Claim("Email", loggedInUser.Email),
			new Claim("Id", loggedInUser.IdNumber.ToString()),
			new Claim("Mobile", loggedInUser.MobileNumber.ToString()),
			new Claim("Dob", loggedInUser.Dob.ToLongDateString()),
			new Claim("ResidentialAddress", loggedInUser.ResidentialAddress),
		};

		var token = new JwtSecurityToken
		(
			issuer: _config.GetSection("Jwt:Issuer").Value,
			audience: _config.GetSection("Jwt:Audience").Value,
			claims: claims,
			expires: DateTime.UtcNow.AddDays(60),
			notBefore: DateTime.UtcNow,
			signingCredentials: new SigningCredentials(
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value)),
				SecurityAlgorithms.HmacSha256)
		);

		var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
		
		if (tokenString.IsNullOrEmpty()) {
			throw new ValidationTestException("Failed to generate a Jwt token, please try again");
		}
		
		return tokenString;
	}
}