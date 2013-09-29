using System;

namespace Mono.Mlo
{
	public class WhereClause
	{
		
		public virtual ILogicalCondition Condition {get;set;}
		
		public WhereClause ()
		{
		}
		
		public virtual string ToQueryString ()
		{
			return (Condition == null ? "" : "WHERE " + Condition.ToQueryString ());
		}
	}
}

