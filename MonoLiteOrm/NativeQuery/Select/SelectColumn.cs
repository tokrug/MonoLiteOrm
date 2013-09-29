using System;

namespace Mono.Mlo
{
	public class SelectColumn
	{
		
		public virtual IQueryExpression Select {get;set;}
		public virtual string Alias {get;set;}
		
		public SelectColumn ()
		{
		}
		
		public virtual string ToQueryString ()
		{
			return Select.ToQueryString() + (Alias == null ? "" : " " + Alias);
		}
	}
}

