using Microsoft.Extensions.DependencyInjection;
using TinyCRM.Application.Interfaces;
using TinyCRM.Application.Services;
using TinyCRM.Domain.Interfaces;
using TinyCRM.Infra.Data.Repositories;

namespace TinyCRM.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<INaturalPersonService, NaturalPersonService>();
            services.AddScoped<ILegalPersonService, LegalPersonService>();

            services.AddScoped<INaturalPersonRepository, NaturalPersonRepository>();
            services.AddScoped<ILegalPersonRepository, LegalPersonRepository>();
        }
    }
}
