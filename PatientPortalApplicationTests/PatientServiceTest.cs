using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using PatientDomain.Model;
using PatientPortalApplication;
using PatientPortalRepository;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;

namespace PatientPortalApplicationTests
{
    public class PatientServiceTest
    {
        public IFormFile formFile; 

        [SetUp]
        public void Setup()
        {
            var fileName = "Patients.csv";
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);
            var bytes = File.ReadAllBytes(path);
            formFile = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", fileName);
        }

        /// <summary>
        /// An integration test for our CSV upload feature
        /// </summary>
        [TestCase("[{\"Id\":0,\"FirstName\":\"Clark\",\"LastName\":\"Kent\",\"Birthday\":\"1984-02-29T00:00:00\",\"Gender\":\"M\"},{\"Id\":0,\"FirstName\":\"Diana\",\"LastName\":\"Prince\",\"Birthday\":\"1976-03-22T00:00:00\",\"Gender\":\"F\"},{\"Id\":0,\"FirstName\":\"Tony\",\"LastName\":\"Stark\",\"Birthday\":\"1970-05-29T00:00:00\",\"Gender\":\"M\"},{\"Id\":0,\"FirstName\":\"Carol\",\"LastName\":\"Denvers\",\"Birthday\":\"1968-04-24T00:00:00\",\"Gender\":\"F\"}]")]
        public async Task UploadPatientRecordCSV(string expectedJSONString)
        {
            //Arrange
            var expectedPatients = JsonConvert.DeserializeObject<List<Patient>>(expectedJSONString);
            var token = new CancellationToken();
            var options = new DbContextOptionsBuilder<PatientContext>().Options;
            var mockPatientContext = new Mock<PatientContext>(options);
            var subject = new PatientService(mockPatientContext.Object);

            //Act
            await subject.UploadPatientRecordCSV(new List<IFormFile> { formFile }, token);
            
            System.Linq.Expressions.Expression<Func<List<Patient>, bool>> match = c => c.SequenceEqual(expectedPatients, new PatientEqualityComparer());

            //Assert
            mockPatientContext.Verify(v => v.AddRangeAsync(It.Is(match), token));
        }

    }

    // There are probably helper class that we can use here 😅
    class PatientEqualityComparer : IEqualityComparer<Patient>
    {
        public bool Equals(Patient b1, Patient b2)
        {
            return b1.FirstName == b2.FirstName &&
                b1.LastName == b2.LastName &&
                b1.Id == b2.Id &&
                b1.Birthday == b2.Birthday &&
                b1.Gender == b2.Gender;
        }

        public int GetHashCode([DisallowNull] Patient obj)
        {
            return obj.GetHashCode();
        }
    }
}