using System;

namespace Mono.Ormo
{
	// marks the id field, mandatory
	[AttributeUsage(AttributeTargets.Field)]
	public class Id : System.Attribute
	{
		public Id ()
		{
		}
	}
}

