using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
