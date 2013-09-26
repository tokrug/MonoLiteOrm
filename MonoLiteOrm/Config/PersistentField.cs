using System;
using System.Reflection;

namespace Mono.Mlo
{
	public class PersistentField
	{
		
		public virtual FieldInfo Field {get;set;}
		public virtual bool IsId {get;set;}
		
		public PersistentField ()
		{
		}
		
	}
}

