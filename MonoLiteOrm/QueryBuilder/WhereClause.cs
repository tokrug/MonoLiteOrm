using System;

namespace Mono.Mlo
{
	public class WhereClause
	{
		
		public virtual EqualCondition Equality {get;set;}
		
		public WhereClause ()
		{
		}
		
		public virtual string ToQueryString ()
		{
			return Equality.ToQueryString ();
		}
	}
}

