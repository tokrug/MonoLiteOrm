using System;

namespace Mono.Mlo
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

