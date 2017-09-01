using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.RequestModel
{
    public class RegistrationRequestModel
    {
		public string Login { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public string Country { get; set; }
		public string Phone { get; set; }
		public string Year { get; set; }
    }
}
