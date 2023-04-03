using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PatientPortalRepository
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddPatientRepoDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<PatientContext>(options => options.UseSqlServer(connectionString));
            return services;
        }
    }
}
