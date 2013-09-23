using System;

namespace Mono.Mlo
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

