using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Meddlingwithfire.Common.Example
{
	public class ContactFormInputModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Birthdate { get; set; }
		public string EmailAddress { get; set; }
		public bool SignupForNewsletter { get; set; }
	}
}
