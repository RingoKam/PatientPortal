using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PatientDomain.Model;
using PatientPortalApplication.Interfaces;

namespace PatientPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            this._patientService = patientService;
        }

        [HttpGet]
        public async Task<PaginatedRecord<Patient>> PatientRecords(string? sortField = null, SortOrder? sortOrder = null, string? filterByPatientName = null, int pageIndex = 0, int pageSize = 10)
        {
            return await _patientService.GetList(sortField, sortOrder, filterByPatientName, pageIndex, pageSize);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePatientRecord([FromBody] Patient patient)
        {
            await _patientService.UpdatePatientRecord(patient);
            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> FileUpload(List<IFormFile> files, CancellationToken cancellationToken)
        {
            await _patientService.UploadPatientRecordCSV(files, cancellationToken);
            return Ok("Files successfully uploaded and saved");
        }
    }
}
