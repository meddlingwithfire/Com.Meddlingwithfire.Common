using System;
using System.Collections.Generic;
using System.Linq;

namespace Com.Meddlingwithfire.Common.Validation
{
	public class ValidationResultSet
	{
		private Dictionary<string, ValidationFieldResults> _results;

		public ValidationResultSet()
		{
            _results = new Dictionary<string, ValidationFieldResults>();
		}

		public void Add(ValidationFieldResults result)
		{
			if (_results.ContainsKey(result.FieldName))
			{
				throw new ArgumentException("This result reference is already in the set.", result.FieldName);
			}
			_results.Add(result.FieldName, result);
		}

		public void AddError(string fieldName, string errorMessage)
		{
			ValidationFieldResults result = _results[fieldName];
			if (result == null)
			{
				result = new ValidationFieldResults(fieldName, errorMessage);
                _results.Add(fieldName, result);
			}
			else
			{
				result.AddError(errorMessage);
			}
		}

		public void AddError(string fieldName, string errorFormat, params object[] args)
		{
			string message = string.Format(errorFormat, args);
			AddError(fieldName, message);
		}

		public bool HasErrorsForField(string fieldName)
		{
            return _results.ContainsKey(fieldName);
		}

		public bool HasErrors
		{
			get
			{
				foreach (ValidationFieldResults result in _results.Values)
				{
					if (result.HasErrors)
					{
						return true;
					}
				}

				return false;
			}
		}

		public IEnumerable<string> GetAllErrorMessages()
		{
			List<string> messages = new List<string>();
			foreach (ValidationFieldResults result in _results.Values)
			{
				IEnumerable<string> resultMessages = result.GetAllMessages();
				foreach (string message in resultMessages)
				{
					messages.Add(message);
				}
			}
			return messages;
		}
	}
}