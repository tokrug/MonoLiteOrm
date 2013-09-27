using System;

namespace Mono.Mlo
{
	public class FromClause
	{
		
		public virtual TableReference Source {get;set;}
		
		public FromClause ()
		{
		}
		
		public virtual string ToQueryString() {
			return null;	
		}
	}
}

