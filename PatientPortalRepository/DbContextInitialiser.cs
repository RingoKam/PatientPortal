using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientPortalRepository
{
    public class DbContextInitialiser
    {
        private PatientContext _patientContext;

        public DbContextInitialiser(PatientContext patientContext)
        {
            this._patientContext = patientContext;
        }

        public async Task Perform()
        {
            _patientContext.Database.EnsureCreated();
            await _patientContext.Database.MigrateAsync();
        }
    }
}
