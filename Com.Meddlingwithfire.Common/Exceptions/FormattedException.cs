using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Meddlingwithfire.Common.Exceptions
{
    public class FormattedException : Exception
    {
		public FormattedException(string format, params object[] args) : base(string.Format(format, args))
		{

		}
    }
}
