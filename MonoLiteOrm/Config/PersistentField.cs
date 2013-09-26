using System;
using System.Reflection;

namespace Mono.Mlo
{
	public class PersistentField
	{
		
		public FieldInfo Field {get;set;}
		public bool IsId {get;set;}
		
		public PersistentField ()
		{
		}
		
	}
}

