using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using PatientDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientPortalApplication.Interfaces
{
    public interface IPatientService
    {
        public Task<PaginatedRecord<Patient>> GetList(string? sortField = null, SortOrder? sortOrder = null, string? filterByPatientName = null, int pageIndex = 0, int pageSize = 10);
        public Task UploadPatientRecordCSV(List<IFormFile> files, CancellationToken cancellationToken);
        public Task<Patient> UpdatePatientRecord(Patient patient);
    }
}
