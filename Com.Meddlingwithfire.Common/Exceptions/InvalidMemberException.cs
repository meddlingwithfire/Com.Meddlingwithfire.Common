using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Meddlingwithfire.Common.Exceptions
{
    public class InvalidMemberException : Exception
    {
        public InvalidMemberException(string argumentName)
            : base(string.Format("{0} is an invalid member.", argumentName))
		{

		}
    }
}
