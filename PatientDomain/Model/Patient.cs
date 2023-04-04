using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDomain.Model
{
    public class Patient
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings =false)]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
    }
}
