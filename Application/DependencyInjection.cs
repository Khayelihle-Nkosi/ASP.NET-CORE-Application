using System.Reflection;
using Application.Common.Interfaces;
using Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection {
	
	public static IServiceCollection AddApplication(this IServiceCollection services) {
		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

		services.AddScoped<IAccountService, AccountService>();
		services.AddScoped<IAccountHolderService, AccountHolderService>();
		return services;
	}

}