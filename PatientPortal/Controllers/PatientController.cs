using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<IActionResult> FileUpload(List<IFormFile> files, CancellationToken cancellationToken)
        {
            await _patientService.UploadPatientRecordCSV(files, cancellationToken);
            return Ok("Files successfully uploaded and saved");
        }
    }
}
