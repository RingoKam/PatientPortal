using CsvHelper;
using Microsoft.AspNetCore.Http;
using PatientPortalRepository;
using System.Globalization;
using PatientPortalApplication.Mapper;
using PatientDomain.Model;
using PatientPortalApplication.Interfaces;

namespace PatientPortalApplication
{
    public class PatientService : IPatientService
    {
        private PatientContext _context { get; }

        public PatientService(PatientContext patientContext)
        {
            _context = patientContext;
        }

        public async Task UploadPatientRecordCSV(List<IFormFile> files, CancellationToken cancellationToken)
        {
            var recordsToBeAdded = ParseCSV(files);
            await _context.AddRangeAsync(recordsToBeAdded, cancellationToken);
            _context.SaveChanges();
        }

        private List<Patient> ParseCSV(List<IFormFile> files)
        {
            var recordsToBeAdded = new List<Patient>();
            foreach (var file in files)
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<PatientMapper>();
                    var result = csv.GetRecords<Patient>().Select((r) => new Patient
                    {
                        FirstName = r.FirstName,
                        LastName = r.LastName,
                        Birthday = r.Birthday,
                        Gender = r.Gender
                    }).ToList();
                    recordsToBeAdded.AddRange(result);
                }
            }
            return recordsToBeAdded;
        }
    }
}