using System.Collections.Generic;
using Com.Meddlingwithfire.Common.Exceptions;

namespace Com.Meddlingwithfire.Common.Validation
{
	public class ValidationFieldResults
	{
		public string FieldName { get; private set; }
		private List<string> _messages;

		public ValidationFieldResults(string fieldName, params string[] messages)
		{
			if (string.IsNullOrEmpty(fieldName))
			{
				throw new ArgumentRequiredException("fieldName");
			}
			FieldName = fieldName;

			_messages = new List<string>();
			foreach (string message in messages)
			{
				_messages.Add(message);
			}
		}

		public void AddError(string message)
		{
			_messages.Add(message);
		}

		public bool HasErrors
		{
			get
			{
				return _messages.Count > 0;
			}
		}

		public IEnumerable<string> GetAllMessages()
		{
			return _messages;
		}
	}
}
