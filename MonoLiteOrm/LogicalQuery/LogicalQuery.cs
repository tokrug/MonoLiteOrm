using System;
using System.Collections.ObjectModel;

namespace Mono.Mlo
{
	public class LogicalQuery
	{
		
		public virtual ReadOnlyCollection<LogicalQueryTable> LogicalTables {get;set;}
		public virtual string NativeQuery {get;set;}
		
		public LogicalQuery ()
		{
		}
	}
}

