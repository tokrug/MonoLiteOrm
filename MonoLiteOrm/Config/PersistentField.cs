using System;
using System.Reflection;

namespace Mono.Mlo
{
	public class PersistentField
	{
		
		public virtual FieldInfo ClassField {get;set;}
		public virtual bool IsId {get;set;}
		
		public PersistentField ()
		{
		}
		
	}
}

