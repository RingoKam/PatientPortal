using CsvHelper;
using Microsoft.AspNetCore.Http;
using PatientPortalRepository;
using System.Globalization;
using PatientPortalApplication.Mapper;
using PatientDomain.Model;
using PatientPortalApplication.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
            // since there can be many rows of record being inserted here. we can probably use bulk insert to speed things up
            // maybe this? https://github.com/borisdj/EFCore.BulkExtensions
            await _context.AddRangeAsync(recordsToBeAdded, cancellationToken);
            _context.SaveChanges();
        }

        public async Task<PaginatedRecord<Patient>> GetList(string? sortField = null, SortOrder? sortOrder = null, string? filterByPatientName = null, int pageIndex = 0, int pageSize = 10)
        {
            var patientsQuery = from s in _context.Patients select s;

            // Filter
            if (!String.IsNullOrEmpty(filterByPatientName))
            {
                patientsQuery = patientsQuery.Where(s => s.LastName.Contains(filterByPatientName)
                                       || s.FirstName.Contains(filterByPatientName));
            }

            //Sort
            if (!string.IsNullOrEmpty(sortField) && sortOrder != null)
            {
                switch (sortField)
                {
                    case "firstName":
                        patientsQuery = sortOrder == SortOrder.Descending
                            ? patientsQuery.OrderByDescending(s => s.FirstName)
                            : patientsQuery.OrderBy(s => s.FirstName);
                        break;
                    case "lastName":
                        patientsQuery = sortOrder == SortOrder.Descending
                            ? patientsQuery.OrderByDescending(s => s.LastName)
                            : patientsQuery.OrderBy(s => s.LastName);
                        break;
                    case "gender":
                        patientsQuery = sortOrder == SortOrder.Descending
                            ? patientsQuery.OrderByDescending(s => s.Gender)
                            : patientsQuery.OrderBy(s => s.Gender);
                        break;
                    case "birthDay":
                        patientsQuery = sortOrder == SortOrder.Descending
                            ? patientsQuery.OrderByDescending(s => s.Birthday)
                            : patientsQuery.OrderBy(s => s.Birthday);
                        break;
                    case "id":
                        patientsQuery = sortOrder == SortOrder.Descending
                            ? patientsQuery.OrderByDescending(s => s.Id)
                            : patientsQuery.OrderBy(s => s.Id);
                        break;
                }
            }

            var patientRecords = await PaginatedList<Patient>.CreateAsync(patientsQuery, pageIndex, pageSize);
            var result = new PaginatedRecord<Patient>
            {
                Data = patientRecords,
                HasNextPage = patientRecords.HasNextPage,
                HasPreviousPage = patientRecords.HasPreviousPage,
                Page = patientRecords.PageIndex,
                PerPage = patientRecords.PageSize,
                TotalPages = patientRecords.TotalPages,
                Length = patientRecords.Length
            };

            return result;
        }

        public async Task<Patient> UpdatePatientRecord(Patient patient)
        {
            var recordToUpdate = await _context.Patients.FirstOrDefaultAsync(s => s.Id == patient.Id);

            if (recordToUpdate == null)
            {
                throw new Exception("Record not found");
            }

            recordToUpdate.Birthday = patient.Birthday;
            recordToUpdate.FirstName = patient.FirstName;
            recordToUpdate.LastName = patient.LastName;
            recordToUpdate.Gender = patient.Gender;

            await _context.SaveChangesAsync();
            return recordToUpdate;
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