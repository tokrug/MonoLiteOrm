using System;

namespace Mono.Mlo
{
	// marker for classes than are to be managed by the ORM
	[System.AttributeUsage(System.AttributeTargets.Class)]
	public class Entity : System.Attribute
	{
		public Entity ()
		{
		}
	}
}

