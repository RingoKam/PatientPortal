using CsvHelper.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace PatientPortalApplication.Mapper
{
    internal class PatientMapper : ClassMap<PatientDomain.Model.Patient>
    {
        public PatientMapper()
        {
            Map(m => m.FirstName).Name("First Name", "FirstName").Validate(f => {
                return !string.IsNullOrWhiteSpace(f.Field);
            });
            Map(m => m.LastName).Name("Last Name", "LastName").Validate(f => {
                return !string.IsNullOrWhiteSpace(f.Field);
            });
            Map(m => m.Birthday).Name("Birthday");
            Map(m => m.Gender).Name("Gender");
        }
    }
}
