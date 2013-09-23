using System;

namespace Mono.Ormo
{
	// marker, do not persist
	[AttributeUsage(AttributeTargets.Field)]
	public class Transient : System.Attribute
	{
		public Transient ()
		{
		}
	}
}

