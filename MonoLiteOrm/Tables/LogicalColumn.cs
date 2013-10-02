using System;

namespace Mono.Mlo
{
	public class LogicalColumn
	{
		
		public virtual TableColumn ReferencedPhysicalColumn {get;set;}
		
		public virtual LogicalTable Table {get;set;}
		// unique name (can be used as alias in queries)
		public virtual string Name {get;set;}
		
		public LogicalColumn ()
		{
		}
	}
}

