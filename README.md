Com.Meddlingwithfire.Common
===========================

Contains some convenient commonly needed classes in .NET projects.

# Example Validation
Validation setup consists of three stages:
- Set up an input model
- Set up a validator
- Test the input model against the validator

## Setting up an input model
Just a POCO.  Doesn't require anything special.

    public class ContactFormInputModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public string EmailAddress { get; set; }
        public bool SignupForNewsletter { get; set; }
    }

## Set up your validator
The easiest way to go is to sub-class the Com.Meddlingwithfire.Common.Validation.BaseValidator class. This provides a virtual method for you to override `void DoValidate(T input)`, defines a set of convenience methods for testing various data points (`MustHaveContent`, `MustBeValidDate`, etc.), and takes care of some basic plumbing to make the validation easier to consume.

    public class ContactFormValidator : BaseValidator<ContactFormInputModel>
    {
        override protected void DoValidate(ContactFormInputModel input)
        {
            MustHaveContent(input.FirstName, "FirstName", "First name");
            MustBeValidEmailAddress(input.EmailAddress, "EmailAddress", "Email address");
            MustBeValidDate(input.Birthdate, "Birthdate", "Birthdate");
        }
    }

## Test the input model against the validator

    ContactFormInputModel input = new ContactFormInputModel()
    { 
        // FirstName is missing
        LastName = "O'neill",
        Birthdate = "10/35/1952", // Invalid date
        EmailAddress = "joneill@", // Invalid email address
        SignupForNewsletter = false
    };
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

Will run and output the following console lines:

    Errors:
    First name is required
    Email address must be a valid email address
    Birthdate must be a valid date
