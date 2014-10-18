using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Meddlingwithfire.Common.Validation;

namespace Com.Meddlingwithfire.Common.Example
{
	public class ContactFormValidator : BaseValidator<ContactFormInputModel>
	{
		override protected void DoValidate(ContactFormInputModel input)
		{
			MustHaveContent(input.FirstName, "FirstName", "First Name");
			MustBeValidEmailAddress(input.EmailAddress, "EmailAddress", "Email Address");
			MustBeValidDate(input.Birthdate, "Birthdate", "Birthdate");
		}
	}
}
