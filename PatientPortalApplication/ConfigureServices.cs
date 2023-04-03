using Microsoft.Extensions.DependencyInjection;
using PatientPortalApplication.Interfaces;
using PatientPortalRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientPortalApplication
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IPatientService, PatientService>();
            return services;
        }
    }
}
