using System.Reflection;
using Application.Features.BackAccount.Query.GetAccountByAccountNumber;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.BankAccount;

namespace Application.Common.Mappings.AccountMapping;

public class AccountMapping {
	
	
	public Mapper BankAccountMapping() {
		var mapper = new Mapper(new MapperConfiguration(cfg => {
			cfg.CreateMap<BankAccount, BankAccountDto>()
			   .ForMember(destination => destination.AccountNumber, options =>
				   options.MapFrom(source => source.AccountNumber))
			   .ForMember(destination => destination.AccountType, options =>
				   options.MapFrom(source => source.AccountType))
			   .ForMember(destination => destination.AvailableBalance, options =>
				   options.MapFrom(source => source.AvailableBalance))
			   .ForMember(destination => destination.AccountStatus, options =>
				   options.MapFrom(source => source.AccountStatus))
			   .ForMember(destination => destination.AccountHolderId, options =>
				   options.MapFrom(source => source.AccountHolderId))
			   .ForMember(destination => destination.AccountName, options =>
				   options.MapFrom(source => source.AccountName));
		}));
		return mapper;
	}
}