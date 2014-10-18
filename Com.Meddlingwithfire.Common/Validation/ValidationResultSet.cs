using System;
using System.Collections.Generic;
using System.Linq;

namespace Com.Meddlingwithfire.Common.Validation
{
	public class ValidationResultSet
	{
		private List<ValidationFieldResults> _list;

		public ValidationResultSet()
		{
			_list = new List<ValidationFieldResults>();
		}

		public void Add(ValidationFieldResults result)
		{
			if (_list.Contains(result))
			{
				throw new ArgumentException("This result reference is already in the set.");
			}
			_list.Add(result);
		}

		public void AddError(string fieldName, string errorMessage)
		{
			ValidationFieldResults result = _list.FirstOrDefault(x => x.FieldName == fieldName);
			if (result == null)
			{
				result = new ValidationFieldResults(fieldName, errorMessage);
				_list.Add(result);
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
			return _list.Any(x => x.FieldName == fieldName);
		}

		public bool HasErrors
		{
			get
			{
				foreach (ValidationFieldResults result in _list)
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
			foreach (ValidationFieldResults result in _list)
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