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
		override protected void DoValidate()
		{
            MustHaveContent(x => x.FirstName);
			MustHaveContent(LastInput.FirstName, "FirstName", "First Name");
            MustBeValidEmailAddress(LastInput.EmailAddress, "EmailAddress", "Email Address");
            MustBeValidDate(LastInput.Birthdate, "Birthdate", "Birthdate");
		}
	}
}
