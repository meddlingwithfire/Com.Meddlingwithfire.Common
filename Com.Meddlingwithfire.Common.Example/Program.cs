using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Meddlingwithfire.Common.Validation;

namespace Com.Meddlingwithfire.Common.Example
{
	class Program
	{
		static void Main(string[] args)
		{
			TestValidInputModel();
			TestInvalidInputModel();
			Console.ReadKey();
		}

		static void TestValidInputModel()
		{
			Console.WriteLine("Testing valid input model...");
			ContactFormInputModel input = new ContactFormInputModel()
			{
				FirstName = "Jack",
				LastName = "O'neill",
				Birthdate = "10/20/1952",
				EmailAddress = "joneill@us.mil",
				SignupForNewsletter = false
			};
			ValidateInputModel(input);
		}

		static void TestInvalidInputModel()
		{
			Console.WriteLine("Testing invalid input model...");
			ContactFormInputModel input = new ContactFormInputModel()
			{ 
				// FirstName is missing
				LastName = "O'neill",
				Birthdate = "10/35/1952", // Invalid date
				EmailAddress = "joneill@", // Invalid email address
				SignupForNewsletter = false
			};
			ValidateInputModel(input);
		}

		static void ValidateInputModel(ContactFormInputModel input)
		{
			ContactFormValidator validator = new ContactFormValidator();
			ValidationResultSet results = validator.Validate(input);
			if (results.HasErrors)
			{
				Console.WriteLine("Errors:");
				IEnumerable<string> messages = results.GetAllErrorMessages();
				foreach (string message in messages)
				{
					Console.WriteLine(message);
				}
			}
			else
			{
				Console.WriteLine("No errors!");
			}
		}
	}
}
