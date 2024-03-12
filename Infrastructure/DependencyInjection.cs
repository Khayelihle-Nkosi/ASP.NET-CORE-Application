using Application.Common.Interfaces;
using Application.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection {
	
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
		services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(
				configuration.GetConnectionString("DefaultConnection")));

		services.AddScoped<IAccountsRepository, AccountRepository>();
		services.AddScoped<IAccountHolderRepository, AccountHolderRepository>();
		services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		return services;
	}

}