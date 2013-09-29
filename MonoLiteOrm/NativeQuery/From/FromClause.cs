using System;

namespace Mono.Mlo
{
	public class FromClause
	{
		
		public virtual ITableExpression Source {get;set;}
		
		public FromClause ()
		{
		}
		
		public virtual string ToQueryString() {
			return "FROM " + Source.ToQueryString();
		}
	}
}

