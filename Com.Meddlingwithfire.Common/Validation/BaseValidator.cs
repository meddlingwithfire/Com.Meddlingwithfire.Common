using Com.Meddlingwithfire.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Com.Meddlingwithfire.Common.Extensions;
using System.Text.RegularExpressions;

namespace Com.Meddlingwithfire.Common.Validation
{
    /// <summary>
    /// Abstract class for specific validators to inherit from.
    /// </summary>
	public abstract class BaseValidator<T>
	{
		public ValidationResultSet LastResults { get; private set; }
        protected T LastInput { get; private set; }

		public BaseValidator()
		{

		}

        /// <summary>
        /// Validates the object using the specified rules.
        /// </summary>
		public ValidationResultSet Validate(T input)
		{
			LastResults = new ValidationResultSet();
            LastInput = input;
			DoValidate();
			return LastResults;
		}

        /// <summary>
        /// For subclasses to override with their checks.
        /// </summary>
        abstract protected void DoValidate();

        /// <summary>
        /// Gets the value from an Expression.  
        /// For those concerned about reflection performance, this added a few ten thousandths of a second to the overall operation.
        /// </summary>
        protected object GetExpressionValue(MemberExpression expression)
        {
            object value = null;
            var property = expression.Member as PropertyInfo;
            if (property != null)
            {
                value = property.GetValue(LastInput);
            }

            return value;
        }

        /// <summary>
        /// If a pretty name field is not passed in, we will use the field name with some fanciness added.
        /// </summary>
        /// <returns></returns>
        protected virtual string DeterminePrettyFieldName(string fieldName, string prettyFieldName)
        {
            return !string.IsNullOrEmpty(prettyFieldName) ? prettyFieldName : fieldName.CamelCaseToWords();
        }

        protected void MustHaveContent(Expression<Func<T, object>> keySelector, string prettyFieldName = null)
        {
            MemberExpression expression = keySelector.Body as MemberExpression;
            MustHaveContent(
                GetExpressionValue(expression), 
                expression.Member.Name, 
                DeterminePrettyFieldName(expression.Member.Name, prettyFieldName)
            );
        }

		protected void MustHaveContent(object fieldValue, string fieldName, string prettyFieldName)
		{
			if (fieldValue == null || string.IsNullOrEmpty(fieldValue.ToString()))
			{
				string message = string.Format("{0} is required", prettyFieldName);
				LastResults.AddError(fieldName, message);
			}
		}

        protected void MustBeValidDate(Expression<Func<T, object>> keySelector, string prettyFieldName = null)
        {
            MemberExpression expression = keySelector.Body as MemberExpression;
            MustBeValidDate(
                GetExpressionValue(expression), 
                expression.Member.Name, 
                DeterminePrettyFieldName(expression.Member.Name, prettyFieldName)
            );
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

        protected void MustBeDecimalOrNull(Expression<Func<T, object>> keySelector, string prettyFieldName = null)
        {
            MemberExpression expression = keySelector.Body as MemberExpression;
            MustBeDecimalOrNull(
                GetExpressionValue(expression), 
                expression.Member.Name, 
                DeterminePrettyFieldName(expression.Member.Name, prettyFieldName)
            );
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

        protected void MustBeDecimal(Expression<Func<T, object>> keySelector, string prettyFieldName = null)
        {
            MemberExpression expression = keySelector.Body as MemberExpression;
            MustBeDecimal(
                GetExpressionValue(expression), 
                expression.Member.Name, 
                DeterminePrettyFieldName(expression.Member.Name, prettyFieldName)
            );
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

        protected void MustBeInteger(Expression<Func<T, object>> keySelector, string prettyFieldName = null)
        {
            MemberExpression expression = keySelector.Body as MemberExpression;
            MustBeInteger(
                GetExpressionValue(expression), 
                expression.Member.Name, 
                DeterminePrettyFieldName(expression.Member.Name, prettyFieldName)
            );
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

        protected void MustBeIntegerOrEmpty(Expression<Func<T, object>> keySelector, string prettyFieldName = null)
        {
            MemberExpression expression = keySelector.Body as MemberExpression;
            MustBeIntegerOrEmpty(
                GetExpressionValue(expression), 
                expression.Member.Name, 
                DeterminePrettyFieldName(expression.Member.Name, prettyFieldName)
            );
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

        protected void MustBeGreaterThan(Expression<Func<T, object>> keySelector, int mustBeGreaterThan, string prettyFieldName = null)
        {
            MemberExpression expression = keySelector.Body as MemberExpression;
            MustBeGreaterThan(
                GetExpressionValue(expression), 
                expression.Member.Name, 
                DeterminePrettyFieldName(expression.Member.Name, prettyFieldName), 
                mustBeGreaterThan
            );
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

        protected void MustBeLessThan(Expression<Func<T, object>> keySelector, int mustBeLessThan, string prettyFieldName = null)
        {
            MemberExpression expression = keySelector.Body as MemberExpression;
            MustBeLessThan(
                GetExpressionValue(expression), 
                expression.Member.Name, 
                DeterminePrettyFieldName(expression.Member.Name, prettyFieldName), 
                mustBeLessThan
            );
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

        protected void MustBeValidEmailAddress(Expression<Func<T, object>> keySelector, string prettyFieldName = null)
        {
            MemberExpression expression = keySelector.Body as MemberExpression;
            MustBeValidEmailAddress(
                GetExpressionValue(expression),
                expression.Member.Name,
                DeterminePrettyFieldName(expression.Member.Name, prettyFieldName)
            );
        }

        // Regex Source: http://jqueryvalidation.org/
        protected const string EMAILREGEX = @"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
        protected void MustBeValidEmailAddress(object fieldValue, string fieldName, string prettyFieldName)
		{
			string message = string.Format("{0} must be a valid email address", prettyFieldName);
			if (fieldValue == null)
			{
				LastResults.AddError(fieldName, message);
			}
			else
			{
                if (!Regex.IsMatch(fieldValue.ToString(), EMAILREGEX, RegexOptions.IgnoreCase))
				{
					LastResults.AddError(fieldName, message);
				}
			}
		}
	}
}