using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Meddlingwithfire.Common.Exceptions
{
	public class ArgumentRequiredException : Exception
	{
		public ArgumentRequiredException(string argumentName)
			: base(string.Format("{0} is a required argument.", argumentName))
		{

		}
	}
}
