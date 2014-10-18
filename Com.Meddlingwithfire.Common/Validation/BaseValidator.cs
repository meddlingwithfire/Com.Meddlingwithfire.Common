using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Com.Meddlingwithfire.Common.Validation
{
	public class BaseValidator<T>
	{
		public ValidationResultSet LastResults { get; private set; }

		public BaseValidator()
		{

		}

		public ValidationResultSet Validate(T input)
		{
			LastResults = new ValidationResultSet();
			DoValidate(input);
			return LastResults;
		}

		virtual protected void DoValidate(T input)
		{
			// For subclasses to override with their checks
		}

		protected void MustHaveContent(object fieldValue, string fieldName, string prettyFieldName)
		{
			if (fieldValue == null || string.IsNullOrEmpty(fieldValue.ToString()))
			{
				string message = string.Format("{0} is required", prettyFieldName);
				LastResults.AddError(fieldName, message);
			}
		}

		protected void MustBeValidDate(object fieldValue, string fieldName, string prettyFieldName)
		{
			MustHaveContent(fieldValue, fieldName, prettyFieldName);
			if (fieldValue != null)
			{
				if (fieldValue is DateTime) // Valid value
				{ }
				else
				{
					DateTime temp;
					if (!DateTime.TryParse(fieldValue.ToString(), out temp))
					{
						string message = string.Format("{0} must be a valid date", prettyFieldName);
						LastResults.AddError(fieldName, message);
					}
				}
			}
		}

		protected void MustBeDecimalOrNull(object fieldValue, string fieldName, string prettyFieldName)
		{
			if (fieldValue == null) // Valid value
			{ }
			else
			{
				if ((fieldValue is decimal)) // Valid value
				{ }
				else
				{
					decimal temp;
					if (!decimal.TryParse(fieldValue.ToString(), out temp))
					{
						string message = string.Format("{0} must be a numeric decimal", prettyFieldName);
						LastResults.AddError(fieldName, message);
					}
				}
			}
		}

		protected void MustBeDecimal(object fieldValue, string fieldName, string prettyFieldName)
		{
			string message = string.Format("{0} must have a valid decimal value", prettyFieldName);
			if (fieldValue == null)
			{
				LastResults.AddError(fieldName, message);
			}
			else if (fieldValue is decimal) // Valid value
			{ }
			else
			{
				string valueAsString = fieldValue.ToString();
				if (string.IsNullOrEmpty(fieldValue.ToString()))
				{
					LastResults.AddError(fieldName, message);
				}
				else
				{
					decimal temp;
					if (!decimal.TryParse(fieldValue.ToString(), out temp))
					{
						LastResults.AddError(fieldName, message);
					}
				}
			}
		}

		protected void MustBeInteger(object fieldValue, string fieldName, string prettyFieldName)
		{
			string message = string.Format("{0} must be a valid number", prettyFieldName);
			if (fieldValue == null)
			{
				LastResults.AddError(fieldName, message);
			}
			else if (fieldValue is int)
			{ }
			else
			{
				int temp;
				if (!int.TryParse(fieldValue.ToString(), out temp))
				{
					LastResults.AddError(fieldName, message);
				}
			}
		}

		protected void MustBeIntegerOrEmpty(object fieldValue, string fieldName, string prettyFieldName)
		{
			if (fieldValue == null) // Valid value
			{ }
			else if (fieldValue is int) // Valid value
			{ }
			else if (string.IsNullOrEmpty(fieldValue.ToString())) // Valid value
			{ }
			else
			{
				int temp;
				if (!int.TryParse(fieldValue.ToString(), out temp))
				{
					string message = string.Format("{0} must be a valid number", prettyFieldName);
					LastResults.AddError(fieldName, message);
				}
			}
		}

		protected void MustBeGreaterThan(object fieldValue, string fieldName, string prettyFieldName, int mustBeGreaterThan)
		{
			int? valueAsInt = null;
			if (fieldValue is int)
			{
				valueAsInt = (int)fieldValue;
			}
			else
			{
				int temp;
				if (int.TryParse(fieldValue.ToString(), out temp))
				{
					valueAsInt = temp;
				}
			}

			if (!valueAsInt.HasValue)
			{
				string message = string.Format("{0} must be a valid integer greater than {1}", prettyFieldName, mustBeGreaterThan);
				LastResults.AddError(fieldName, message);
			}
			else
			{
				if (valueAsInt.Value <= mustBeGreaterThan)
				{
					string message = string.Format("{0} must be greater than {1}", prettyFieldName, mustBeGreaterThan);
					LastResults.AddError(fieldName, message);
				}
			}
		}

		protected void MustBeLessThan(object fieldValue, string fieldName, string prettyFieldName, int mustBeLessThan)
		{
			int? valueAsInt = null;
			if (fieldValue is int)
			{
				valueAsInt = (int)fieldValue;
			}
			else
			{
				int temp;
				if (int.TryParse(fieldValue.ToString(), out temp))
				{
					valueAsInt = temp;
				}
			}

			if (!valueAsInt.HasValue)
			{
				string message = string.Format("{0} must be a valid integer less than {1}", prettyFieldName, mustBeLessThan);
				LastResults.AddError(fieldName, message);
			}
			else
			{
				if (valueAsInt.Value >= mustBeLessThan)
				{
					string message = string.Format("{0} must be less than {1}", prettyFieldName, mustBeLessThan);
					LastResults.AddError(fieldName, message);
				}
			}
		}

		protected void MustBeValidEmailAddress(object fieldValue, string fieldName, string prettyFieldName)
		{
			string message = string.Format("{0} must be a valid email address", prettyFieldName);
			if (fieldValue == null)
			{
				LastResults.AddError(fieldName, message);
			}
			else
			{
				try
				{
					new MailAddress(fieldValue.ToString());
				}
				catch
				{
					LastResults.AddError(fieldName, message);
				}
			}
		}
	}
}