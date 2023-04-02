using CsvHelper.Configuration;
using PatientDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientPortalApplication.Mapper
{
    internal class PatientMapper : ClassMap<PatientDomain.Model.Patient>
    {
        public PatientMapper()
        {
            Map(m => m.FirstName).Name("First Name", "FirstName");
            Map(m => m.LastName).Name("Last Name", "LastName");
            Map(m => m.Birthday).Name("Birthday");
            Map(m => m.Gender).Name("Gender");
        }
    }
}
