using System;

namespace Mono.Mlo
{
	public class SubqueryTable : ITableExpression
	{
		
		public virtual Query Query {get;set;}
		public virtual string Alias {get;set;}
		
		public SubqueryTable ()
		{
		}
		
		public virtual string ToQueryString() {
			return "(" + Query.ToQueryString() + ")" + (Alias == null ? "" : " " + Alias);
		}
	}
}

