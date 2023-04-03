using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientPortalApplication.Interfaces
{
    public interface IPatientService
    {
        public Task UploadPatientRecordCSV(List<IFormFile> files, CancellationToken cancellationToken);

    }
}
