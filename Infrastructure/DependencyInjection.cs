using Application.Interfaces;
using Domain.Interfaceses;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IValueEntryRepository, ValueEntryRepository>();
        services.AddScoped<IResultRepository, ResultRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}