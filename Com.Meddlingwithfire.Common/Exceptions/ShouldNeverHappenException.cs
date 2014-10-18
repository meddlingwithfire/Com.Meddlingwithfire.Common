using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Meddlingwithfire.Common.Exceptions
{
	public class ShouldNeverHappenException : Exception
	{
		public ShouldNeverHappenException()
			: base("This should never have happened. Figure out what caused this.")
		{

		}
	}
}
