using Microsoft.EntityFrameworkCore;
using PatientDomain.Model;
using System.Collections.Generic;

namespace PatientPortalRepository
{
    public class PatientContext : DbContext
    {
        public PatientContext(DbContextOptions<PatientContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
    }
}