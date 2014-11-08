using Com.Meddlingwithfire.Common.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Com.Meddlingwithfire.Common.Validation
{
	public class ValidationFieldResults
	{
		public string FieldName { get; private set; }
		private List<string> _messages;

        public ValidationFieldResults(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                throw new ArgumentRequiredException("fieldName");
            }
            FieldName = fieldName;

            _messages = new List<string>();
        }

        public bool HasErrors
        {
            get
            {
                return _messages.Any();
            }
        }

        public IEnumerable<string> Messages
        {
            get
            {
                return _messages;
            }
        }

		public ValidationFieldResults(string fieldName, params string[] messages) : this(fieldName)
		{
			foreach (string message in messages)
			{
				_messages.Add(message);
			}
		}

		public void AddError(string message)
		{
			_messages.Add(message);
		}
	}
}
